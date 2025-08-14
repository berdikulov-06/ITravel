using ITravel.Data.IRepositories;
using ITravel.Domain.Commons;
using ITravel.Domain.Entities.Payme;
using ITravel.Domain.Entities;
using ITravel.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITravel.Domain.Enums;
using System.Globalization;
using System.Numerics;
using ITravel.Data.Contexts;

namespace ITravel.Service.Services
{
    public class PaymeService : IPaymeService
    {

        private readonly long time_expired = 43_200_000L;

        private readonly IUnitOfWork _repository;
        private CustomerOrder order;

        public PaymeService(IUnitOfWork unitOfWork)
        {
            this._repository = unitOfWork;
        }

        public async Task AddOrder(order paymeOrders)
        {
            var aa = _repository.PaymeOrderRepository.Create(paymeOrders);
            await _repository.SaveChangesAsync();

        }

        public order GetLastOrder()
        {
            return _repository.PaymeOrderRepository.GetAll().OrderByDescending(that => that.id).FirstOrDefault();
        }

        public async Task<PaymeResponse<CancelTransactionResult>> CancelTransactionAsync(string id_transaction, int reason)
        {
            var transaction = await _repository.PaymeTransRepository.FirstOrDefaultAsync(that => that.paycomId == id_transaction);
            if (transaction != null)
            {
                string date_accept = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.FFFzzz", new CultureInfo("en-GB"));
                DateTimeOffset dateTimeOffset = DateTimeOffset.Parse(date_accept);
                long unixTimestamp = dateTimeOffset.ToUnixTimeMilliseconds();

                if (transaction.state == TransactionState.STATE_IN_PROGRESS)
                {
                    transaction.state = TransactionState.STATE_CANCELED;
                    transaction.cancelTime = unixTimestamp;
                    transaction.reason = (OrderCancelReason)reason;
                    _repository.PaymeTransRepository.Update(transaction);
                    await _repository.SaveChangesAsync();

                    var respone = new PaymeResponse<CancelTransactionResult>()
                    {
                        result = new CancelTransactionResult() { transaction = transaction.paycomId, cancel_time = transaction.cancelTime, state = ((int)transaction.state) }
                    };
                    return respone;
                }
                else if (transaction.state == TransactionState.STATE_DONE)
                {
                    if (transaction.state != TransactionState.STATE_CANCELED)
                    {
                        transaction.state = TransactionState.STATE_POST_CANCELED;
                        transaction.reason = (OrderCancelReason)reason;
                        transaction.cancelTime = unixTimestamp;
                        _repository.PaymeTransRepository.Update(transaction);
                        await _repository.SaveChangesAsync();

                        var respone = new PaymeResponse<CancelTransactionResult>()
                        {
                            result = new CancelTransactionResult() { transaction = transaction.paycomId, cancel_time = transaction.cancelTime, state = ((int)transaction.state) }
                        };
                        return respone;
                    }
                    else
                    {
                        var respone = new PaymeResponse<CancelTransactionResult>()
                        {
                            error = new PaymeError()
                            {
                                Code = -31007,
                                Data = "transaction",
                                Message = "Заказ выполнен. Невозможно отменить транзакцию. Товар или услуга предоставлена покупателю в полном объеме."
                            }
                        };

                        return respone;
                    }
                }
                else
                {
                    var respone = new PaymeResponse<CancelTransactionResult>()
                    {
                        result = new CancelTransactionResult() { transaction = transaction.paycomId, cancel_time = transaction.cancelTime, state = ((int)transaction.state) }
                    };
                    return respone;
                }
            }
            else
            {
                var respone = new PaymeResponse<CancelTransactionResult>()
                {
                    error = new PaymeError()
                    {
                        Code = -31003,
                        Data = "transaction",
                        Message = "Транзакция не найдена."
                    }
                };

                return respone;
            }
        }

        public async Task<PaymeResponse<CheckPerformTransactionResult>> CheckPerformTransactionAsync(decimal amount, Account account)
        {
            bool allow = true;

            var isExistOrder = await _repository.PaymeOrderRepository.FirstOrDefaultAsync(that => that.id == account.order);

            if (isExistOrder != null)
            {
                if (isExistOrder.status == TransactionService.ORDER_NEW)
                {
                    if (isExistOrder.amount == amount)
                    {
                        var response = new PaymeResponse<CheckPerformTransactionResult>()
                        {
                            result = new CheckPerformTransactionResult()
                            {
                                allow = allow
                            }
                        };
                        return response;
                    }
                    else
                    {
                        var response = new PaymeResponse<CheckPerformTransactionResult>()
                        {
                            error = new PaymeError()
                            {
                                Code = MerchantOptions.ERROR_INVALID_AMOUNT,
                                Data = "amount",
                                Message = "Неверная сумма."
                            }
                        };
                        return response;
                    }
                }
                else
                {
                    var response = new PaymeResponse<CheckPerformTransactionResult>()
                    {
                        error = new PaymeError()
                        {
                            Code = MerchantOptions.ERROR_INVALID_ACCOUNT,
                            Data = "order",
                            Message = "The order is not available for payment"
                        }
                    };
                    return response;
                }

            }
            else
            {
                var response = new PaymeResponse<CheckPerformTransactionResult>()
                {
                    error = new PaymeError()
                    {
                        Code = MerchantOptions.ERROR_INVALID_ACCOUNT,
                        Data = "order",
                        Message = "Order not found"
                    }
                };
                return response;
            }
        }

        public async Task<PaymeResponse<CheckTransactionResult>> CheckTransactionAsync(string id_transaction)
        {
            var transaction = await _repository.PaymeTransRepository.FirstOrDefaultAsync(that => that.paycomId == id_transaction);

            if (transaction != null)
            {
                var response = new PaymeResponse<CheckTransactionResult>()
                {
                    result = new CheckTransactionResult()
                    {
                        create_time = transaction.createTime,
                        perform_time = transaction.performTime,
                        cancel_time = transaction.cancelTime,
                        transaction = transaction.paycomId,
                        state = (int?)transaction.state,
                        reason = ((int?)transaction.reason)
                    }
                };
                return response;

            }
            else
            {
                var response = new PaymeResponse<CheckTransactionResult>()
                {
                    error = new PaymeError()
                    {
                        Code = -31003,
                        Data = "transaction",
                        Message = "Транзакция не найдена."
                    }
                };
                return response;
            }
        }

        public async Task<PaymeResponse<CreateTransactionResult>> CreateTransactionAsync(string id_transaction, long time, decimal amount, Account account)
        {
            var transaction = await _repository.PaymeTransRepository.FirstOrDefaultAsync(that => that.paycomId == id_transaction);

            if (transaction == null)
            {
                var result = await CheckPerformTransactionAsync(amount, account);

                if (result.error == null)
                {
                    payme_transaction paymeTrans = new payme_transaction()
                    {
                        paycomId = id_transaction,
                        paycomTime = time,
                        createTime = time,
                        state = TransactionState.STATE_IN_PROGRESS,
                        orderid = account.order, 
                        amount  = amount,
                    };

                    var entity = _repository.PaymeTransRepository.Create(paymeTrans);

                    var order = await _repository.PaymeOrderRepository.FirstOrDefaultAsync(that => that.id == account.order);
                    if ( order != null)
                    {
                        order.status = TransactionService.ORDER_PENDING;

                        _repository.PaymeOrderRepository.Update(order);
                    }

                    await _repository.SaveChangesAsync();

                    var response = new PaymeResponse<CreateTransactionResult>()
                    {
                        result = new CreateTransactionResult()
                        {
                            create_time = paymeTrans.createTime,
                            transaction = paymeTrans.paycomId,
                            state = ((int)paymeTrans.state),
                        }
                    };
                    return response;

                }
                else
                {
                    var response = new PaymeResponse<CreateTransactionResult>()
                    {
                        error = new PaymeError()
                        {
                            Code = result.error.Code,
                            Data = result.error.Data,
                            Message = result.error.Message
                        }
                    };
                    return response;
                }

            }
            else
            {
                if (transaction.state == TransactionState.STATE_IN_PROGRESS)
                {
                    DateTime start = DateTimeOffset.FromUnixTimeMilliseconds(transaction.paycomTime).DateTime;
                    var dateTime = DateTime.SpecifyKind(start, DateTimeKind.Utc);

                    if (DateTime.UtcNow.Subtract(dateTime).TotalMilliseconds > time_expired)
                    {
                        transaction.state = TransactionState.STATE_CANCELED;
                        transaction.reason = OrderCancelReason.TRANSACTION_TIMEOUT;

                        _repository.PaymeTransRepository.Update(transaction);
                        await _repository.SaveChangesAsync();

                        var response = new PaymeResponse<CreateTransactionResult>()
                        {
                            error = new PaymeError()
                            {
                                Code = -31008,
                                Data = "create transaction",
                                Message = "Невозможно выполнить операцию"
                            }
                        };
                        return response;
                    }
                    else
                    {
                        var response = new PaymeResponse<CreateTransactionResult>()
                        {
                            result = new CreateTransactionResult()
                            {
                                create_time = transaction.createTime,
                                transaction = transaction.paycomId,
                                state = ((int)transaction.state),
                            }
                        };
                        return response;
                    }

                }
                else
                {
                    var response = new PaymeResponse<CreateTransactionResult>()
                    {
                        error = new PaymeError()
                        {
                            Code = -31008,
                            Data = "create transaction",
                            Message = "Невозможно выполнить операцию"
                        }
                    };
                    return response;
                }

            }


        }

        public async Task<dynamic> DirectMethod(PaymeRequest request) => request.Method switch
        {
            "CheckPerformTransaction" => await CheckPerformTransactionAsync(request.Params.Amount, request.Params.Account),
            "CheckTransaction" => await CheckTransactionAsync(request.Params.Id),
            "CreateTransaction" => await CreateTransactionAsync(request.Params.Id, request.Params.Time, request.Params.Amount, request.Params.Account),
            "PerformTransaction" => await PerformTransactionAsync(request.Params.Id),
            "CancelTransaction" => await CancelTransactionAsync(request.Params.Id, request.Params.Reason),
            "GetStatement" => await GetStatementAsync(request.Params.From, request.Params.To),
            "ChangePassword" => await ChangePassword(request.Params.Password),
            _ => new PaymeResponse<object>
            {
                error = new PaymeError
                {
                    Code = -32504,
                    Message = "Method not found",
                    Data = "Method"
                }
            },
        };

        public async Task<dynamic> GetStatementAsync(long from, long to)
        {
            var result = new List<GetStatementResult>();
            var transactions = _repository.PaymeTransRepository.Where(that => that.paycomTime>= from && that.paycomTime <= to && that.state == TransactionState.STATE_DONE).ToList();

            if (transactions.Count > 0)
            {
                dynamic dynamicModel = new ExpandoObject();
                dynamicModel.transactions = new List<dynamic>();

                foreach (var transaction in transactions)
                {
                    dynamic dynamicItem = new ExpandoObject();
                    dynamicItem.id = transaction.id;
                    dynamicItem.time = transaction.createTime;

                    var order = await _repository.PaymeOrderRepository.FirstOrDefaultAsync(that => that.id == transaction.orderid);
                    dynamic dynamicPhone = new ExpandoObject();
                    dynamicPhone.phone = order.phone;

                    dynamicItem.amount = order.amount;
                    dynamicItem.account = dynamicPhone;
                    dynamicItem.create_time = transaction.createTime;
                    dynamicItem.perform_time = transaction.performTime;
                    dynamicItem.cancel_time = transaction.cancelTime;
                    dynamicItem.transaction = transaction.id;
                    dynamicItem.state = transaction.state;
                    dynamicItem.reason = transaction.reason;
                    dynamicItem.receivers = new List<dynamic>();

                    dynamicModel.transactions.Add(dynamicItem);
                }


                dynamic dynamicMain = new ExpandoObject();
                dynamicMain.result = dynamicModel;

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicMain);
                return dynamicMain;

            }
            else
            {
                dynamic dynamicModel = new ExpandoObject();
                dynamicModel.transactions = new List<dynamic>();
                dynamic dynamicMain = new ExpandoObject();
                dynamicMain.result = dynamicModel;

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicMain);
                return dynamicMain;
            }
        }

        public async Task<PaymeResponse<PerformTransactionResult>> PerformTransactionAsync(string id_transaction)
        {
            var transaction = await _repository.PaymeTransRepository.FirstOrDefaultAsync(that => that.paycomId == id_transaction);

            if (transaction != null)
            {
                if (transaction.state == TransactionState.STATE_IN_PROGRESS)
                {
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(transaction.paycomTime);
                    string formattedDateTime = dateTimeOffset.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
                    var dateTime = DateTime.Parse(formattedDateTime);

                    if (DateTime.UtcNow.Subtract(dateTime).TotalMilliseconds > time_expired)
                    {
                        transaction.state = TransactionState.STATE_CANCELED;
                        transaction.reason = OrderCancelReason.TRANSACTION_TIMEOUT;
                        _repository.PaymeTransRepository.Update(transaction);
                        await _repository.SaveChangesAsync();

                        var response = new PaymeResponse<PerformTransactionResult>()
                        {
                            error = new PaymeError()
                            {
                                Code = -31008,
                                Data = "transaction",
                                Message = "Невозможно выполнить данную операцию"
                            }
                        };
                        return response;
                    }
                    else
                    {
                        string date_accept = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.FFFzzz", new CultureInfo("en-GB"));
                        DateTimeOffset dateTimeOffset2 = DateTimeOffset.Parse(date_accept);
                        long unixTimestamp = dateTimeOffset2.ToUnixTimeMilliseconds();

                        transaction.state = TransactionState.STATE_DONE;
                        transaction.performTime = unixTimestamp;
                        _repository.PaymeTransRepository.Update(transaction);

                        var order = await _repository.PaymeOrderRepository.FirstOrDefaultAsync(that => that.id == transaction.orderid);
                        if (order != null)
                        {
                            order.status = TransactionService.ORDER_PAID;

                            _repository.PaymeOrderRepository.Update(order);
                        }


                        await _repository.SaveChangesAsync();

                        var response = new PaymeResponse<PerformTransactionResult>()
                        {
                            result = new PerformTransactionResult()
                            {
                                transaction = transaction.paycomId,
                                perform_time = transaction.performTime,
                                state = ((int)transaction.state)
                            }
                        };
                        return response;
                    }
                }
                else if (transaction.state == TransactionState.STATE_DONE)
                {
                    var response = new PaymeResponse<PerformTransactionResult>()
                    {
                        result = new PerformTransactionResult()
                        {
                            transaction = transaction.paycomId,
                            perform_time = transaction.performTime,
                            state = ((int)transaction.state)
                        }
                    };
                    return response;
                }
                else
                {
                    var response = new PaymeResponse<PerformTransactionResult>()
                    {
                        error = new PaymeError()
                        {
                            Code = -31008,
                            Data = "perform transaction",
                            Message = "Невозможно выполнить данную операцию"
                        }
                    };
                    return response;
                }
            }
            else
            {
                var response = new PaymeResponse<PerformTransactionResult>()
                {
                    error = new PaymeError()
                    {
                        Code = -31003,
                        Data = "perform transaction",
                        Message = "Транзакция не найдена."
                    }
                };
                return response;
            }
        }

        private bool IsWithinTimeLimit(long performTime, TimeSpan timeLimit)
        {
            // Convert the performTime from milliseconds to DateTime
            DateTime performDateTime = DateTimeOffset.FromUnixTimeMilliseconds(performTime).UtcDateTime;

            // Calculate the elapsed time since the performTime
            TimeSpan elapsedTime = DateTime.UtcNow - performDateTime;

            // Check if the elapsed time is within the specified time limit
            return elapsedTime <= timeLimit;
        }

        public async Task<PaymeResponse<ChangePasswordResult>> ChangePassword(string password)
        {
            var sett = await _repository.SoftSetting.FirstOrDefaultAsync(that => that.Id > 0);

            if (sett != null)
            {
                sett.PaymeKassPassw = password;

                _repository.SoftSetting.Update(sett);
                await _repository.SaveChangesAsync();

                var response = new PaymeResponse<ChangePasswordResult>()
                {
                    result = new ChangePasswordResult(true)
                };
                return response;
            }
            else
            {
                var response = new PaymeResponse<ChangePasswordResult>()
                {
                    error = new PaymeError()
                    {
                        Code = MerchantOptions.ERROR_INTERNAL_SYSTEM,
                        Data = "Change Password",
                        Message = "Method not allowed"
                    }
                };
                return response;
            }
        }
    }
}
