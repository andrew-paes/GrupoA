/*
 * Image preview script 
 * powered by jQuery (http://www.jquery.com)
 * 
 * written by Alen Grakalic (http://cssglobe.com)
 * 
 * for more info visit http://cssglobe.com/post/1695/easiest-tooltip-and-image-preview-using-jquery
 *
 */
 
this.imagePreview = function(){	
	/* CONFIG */
		
		xOffset = 130;
		yOffset = -300;
		
		// these 2 variable determine popup's distance from the cursor
		// you might want to adjust to get the right result
		
	/* END CONFIG */
	$("img.preview").hover(function(e){
		this.t = this.title;
		this.title = "";	
		var c = (this.t != "") ? "<br/>" + this.t : "";
		$("body").append("<div class='prev'><img width=\"200px\" src='"+ this.src +"' alt='Image preview' />"+ c +"</div>");								 
		$("div.prev")
			.css("top",(e.pageY - xOffset) + "px")
			.css("left", (e.pageX + yOffset) + "px")
			.css("position","absolute")
			.fadeIn("fast");						
    },
	function(){
		this.title = this.t;
		$("div.prev").remove();
    });
    $("img.preview").mousemove(function(e) {
    $("div.prev")
			.css("top",(e.pageY - xOffset) + "px")
			.css("left",(e.pageX + yOffset) + "px");
	});			
};
$("img.preview").mouseout(function() { $("div.prev").remove(); });


// starting the script on page load
$(document).ready(function(){
	imagePreview();
});