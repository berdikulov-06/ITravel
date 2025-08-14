/*
Author       : theme_crazy
Template Name: Tourest - Tour & Travels Agency Template
Version      : 1.0
*/

(function($)
{
	"use strict";
	
	// Preloader
	jQuery(window).on('load', function() {
		preloader();
		
		// Gallery Filter
		if(jQuery('.gallery-outer .gallery-items').length > 0){
			jQuery('.gallery-outer .gallery-items').filterizr();
		}
		jQuery('#filter-list li').on("click", function(){
			jQuery('#filter-list li').removeClass('active');
			jQuery(this).addClass('active');
		});
	});
	
	// Animation section
	 if(jQuery('.wow').length){
		var wow = new WOW(
		  {
			boxClass:     'wow',      // animated element css class (default is wow)
			animateClass: 'animated', // animation css class (default is animated)
			offset:       0,          // distance to the element when triggering the animation (default is 0)
			mobile:       true,       // trigger animations on mobile devices (default is true)
			live:         true       // act on asynchronously loaded content (default is true)
		  }
		);
		wow.init();
	}
	
	// Popup
	jQuery('.venobox').venobox();

	// CounterUp 
	jQuery('.counter').counterUp({
		delay: 10,
		time: 2000
	});


	// Owl Carousel
	$('#tour-carousel').owlCarousel({
	    loop:true,
	    margin:10,
	    responsiveClass:true,
	    responsive:{
	        0:{
	            items:1,
	            nav:true
	        },
	        600:{
	            items:2,
	            nav:false
	        },
	        992:{
	            items:3,
	            nav:true,
	            loop:false
	        }
	    }
	})

	
	jQuery("#sidebar").stick_in_parent();
	
	// Back to top 		
	jQuery('.back-top a').on('click', function(event) {
		jQuery('body,html').animate({scrollTop:0},800);
		return false;
	});
	
	jQuery(window).on('scroll', function() {
		
		// Back to top 
		if(jQuery(this).scrollTop()>150){
			jQuery('.back-top').fadeIn();
		}
		else{
			jQuery('.back-top').fadeOut();
		}
	});
	
	// Preload
	function preloader(){
		jQuery(".preloaderimg").fadeOut();
		jQuery(".preloader").delay(200).fadeOut("slow").delay(200, function(){
			jQuery(this).remove();
		});
	}

	jQuery('.tilt-img').tilt({
		maxTilt:9					
	});
	
	// datepicker
	$('.start-date').datepicker();

	//nice select
	$('select').niceSelect();

	// Rang Slider
	$('#slider-range').slider({
      range: true,
      min: 130,
      max: 500,
      values: [130, 250],
      slide: function (event, ui) {
        $('#amount').val('$' + ui.values[0] + ' - $' + ui.values[1]);
      },
    });
    $('#amount').val('$' + $('#slider-range').slider('values', 0) + ' - $' + $('#slider-range').slider('values', 1));

	// Scroll Animation
	scrollCue.init();
	
})(jQuery);	