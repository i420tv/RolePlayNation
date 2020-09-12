
$('#menuOkay').click(() =>{
	mp.trigger('menuOkayToServer');
});

$('#menuFix').click(() =>{
	mp.trigger('menuFixToServer');
});


$('#menuEngine').click(() =>{
	$("#mainButtons").fadeIn(300, function(){
		$("#mainButtons").hide();
		$("#maint").hide();
		$('#enopt').show();
		$("#engineButtons").fadeIn(300);
		$("#backButton").fadeIn();
	});

});

$('#menuTrans').click(() =>{
	$("#mainButtons").fadeIn(300, function(){
		$("#mainButtons").hide();
		$("#maint").hide();
		$('#tropt').show();
		$("#transmissionButtons").fadeIn(300);
		$("#backButton").fadeIn();
	});
});

$('#menuBrakes').click(() =>{
	$("#mainButtons").fadeIn(300, function(){
		$("#mainButtons").hide();
		$("#maint").hide();
		$('#bropt').show();
		$("#brakesButtons").fadeIn(300);
		$("#backButton").fadeIn();
	});
});

$('#menuSusp').click(() =>{
	$("#mainButtons").fadeIn(300, function(){
		$("#mainButtons").hide();
		$("#maint").hide();
		$('#suopt').show();
		$("#suspensionButtons").fadeIn(300);
		$("#backButton").fadeIn();
	});
});

$('#menuColor').click(() =>{
	$("#mainButtons").fadeIn(300, function(){
		$("#mainButtons").hide();
		$("#maint").hide();
		$('#colopt').show();
		$("#colorOptions").fadeIn(300);
		$("#backButton").fadeIn();
	});
});

$('#menuSpoiler').click(() => {
	$("#mainButtons").fadeIn(300, function () {
		$("#mainButtons").hide();
		$("#maint").hide();
		$('#spoopt').show();
		$("#spoilerButtons").fadeIn(300);
		$("#backButton").fadeIn();
	});
});

$('#menuBbumper').click(() => {
	$("#mainButtons").fadeIn(300, function () {
		$("#mainButtons").hide();
		$("#maint").hide();
		$('#bskopt').show();
		$("#backbumperButtons").fadeIn(300);
		$("#backButton").fadeIn();
	});
});

$('#menuFb').click(() => {
	$("#mainButtons").fadeIn(300, function () {
		$("#mainButtons").hide();
		$("#maint").hide();
		$('#fbopt').show();
		$("#fbButtons").fadeIn(300);
		$("#backButton").fadeIn();
	});
});

$('#menuSkirts').click(() => {
	$("#mainButtons").fadeIn(300, function () {
		$("#mainButtons").hide();
		$("#maint").hide();
		$('#sideopt').show();
		$("#skirtButtons").fadeIn(300);
		$("#backButton").fadeIn();
	});
});

$('#menuBo').click(() => {
	$("#mainButtons").fadeIn(300, function () {
		$("#mainButtons").hide();
		$("#maint").hide();
		$('#boopt').show();
		$("#boostButtons").fadeIn(300);
		$("#backButton").fadeIn();
	});
});

$('#menuTurbo').click(() =>{
	mp.trigger('menuTurboToServer');
});

$('#side1').click(() => {
	mp.trigger('Side1');
});

$('#side2').click(() => {
	mp.trigger('Side2');
});

$('#side3').click(() => {
	mp.trigger('Side3');
});

$('#side4').click(() => {
	mp.trigger('Side4');
});
$('#side5').click(() => {
	mp.trigger('Side5');
});

$('#side6').click(() => {
	mp.trigger('Side6');
});

$('#side7').click(() => {
	mp.trigger('Side7');
});

$('#side8').click(() => {
	mp.trigger('Side8');
});
$('#side9').click(() => {
	mp.trigger('Side9');
});

$('#side10').click(() => {
	mp.trigger('Side10');
});

$('#side11').click(() => {
	mp.trigger('Side11');
});

$('#side12').click(() => {
	mp.trigger('Side12');
});

$('#side13').click(() => {
	mp.trigger('Side13');
});

$('#side14').click(() => {
	mp.trigger('Side14');
});

$('#side15').click(() => {
	mp.trigger('Side15');
});

$('#side16').click(() => {
	mp.trigger('Side16');
});

$('#bo1').click(() => {
	mp.trigger('Bo1');
});

$('#bo2').click(() => {
	mp.trigger('Bo2');
});

$('#bo3').click(() => {
	mp.trigger('Bo3');
});

$('#bo4').click(() => {
	mp.trigger('Bo4');
});
$('#bo5').click(() => {
	mp.trigger('Bo5');
});
	$('#fb1').click(() => {
		mp.trigger('Fb1');
	});

	$('#fb2').click(() => {
		mp.trigger('Fb2');
	});

	$('#fb3').click(() => {
		mp.trigger('Fb3');
	});

	$('#fb4').click(() => {
		mp.trigger('Fb4');
	});
	$('#fb5').click(() => {
		mp.trigger('Fb5');
	});

	$('#fb6').click(() => {
		mp.trigger('Fb6');
	});

	$('#fb7').click(() => {
		mp.trigger('Fb7');
	});

	$('#fb8').click(() => {
		mp.trigger('Fb8');
	});
	$('#fb9').click(() => {
		mp.trigger('Fb9');
	});

	$('#fb10').click(() => {
		mp.trigger('Fb10');
	});

	$('#fb11').click(() => {
		mp.trigger('Fb11');
	});

	$('#fb12').click(() => {
		mp.trigger('Fb12');
	});

	$('#fb13').click(() => {
		mp.trigger('Fb13');
	});

	$('#fb14').click(() => {
		mp.trigger('Fb14');
	});

	$('#fb15').click(() => {
		mp.trigger('Fb15');
	});

	$('#fb16').click(() => {
		mp.trigger('Fb16');
	});

	$('#spo1').click(() => {
		mp.trigger('Spo1');
	});

	$('#spo2').click(() => {
		mp.trigger('Spo2');
	});

	$('#spo3').click(() => {
		mp.trigger('Spo3');
	});

	$('#spo4').click(() => {
		mp.trigger('Spo4');
	});
	$('#spo5').click(() => {
		mp.trigger('Spo5');
	});

	$('#spo6').click(() => {
		mp.trigger('Spo6');
	});

	$('#spo7').click(() => {
		mp.trigger('Spo7');
	});

	$('#spo8').click(() => {
		mp.trigger('Spo8');
	});
	$('#spo9').click(() => {
		mp.trigger('Spo9');
	});

	$('#spo10').click(() => {
		mp.trigger('Spo10');
	});

	$('#spo11').click(() => {
		mp.trigger('Spo11');
	});

	$('#spo12').click(() => {
		mp.trigger('Spo12');
	});

	$('#spo13').click(() => {
		mp.trigger('Spo13');
	});

	$('#spo14').click(() => {
		mp.trigger('Spo14');
	});

	$('#spo15').click(() => {
		mp.trigger('Spo15');
	});

	$('#spo16').click(() => {
		mp.trigger('Spo16');
	});

	$('#bsk1').click(() => {
		mp.trigger('Bsk1');
	});

	$('#bsk2').click(() => {
		mp.trigger('Bsk2');
	});

	$('#bsk3').click(() => {
		mp.trigger('Bsk3');
	});

	$('#bsk4').click(() => {
		mp.trigger('Bsk4');
	});
	$('#bsk5').click(() => {
		mp.trigger('Bsk5');
	});

	$('#bsk6').click(() => {
		mp.trigger('Bsk6');
	});

	$('#bsk7').click(() => {
		mp.trigger('Bsk7');
	});

	$('#bsk8').click(() => {
		mp.trigger('Bsk8');
	});
	$('#bsk9').click(() => {
		mp.trigger('Bsk9');
	});

	$('#bsk10').click(() => {
		mp.trigger('Bsk10');
	});

	$('#bsk11').click(() => {
		mp.trigger('Bsk11');
	});

	$('#bsk12').click(() => {
		mp.trigger('Bsk12');
	});

	$('#bsk13').click(() => {
		mp.trigger('Bsk13');
	});

	$('#bsk14').click(() => {
		mp.trigger('Bsk14');
	});

	$('#bsk15').click(() => {
		mp.trigger('Bsk15');
	});

	$('#bsk16').click(() => {
		mp.trigger('Bsk16');
	});

	$('#ems1').click(() => {
		mp.trigger('ems1');
	});

	$('#ems2').click(() => {
		mp.trigger('ems2');
	});
	$('#ems3').click(() => {
		mp.trigger('ems3');
	});
	$('#ems4').click(() => {
		mp.trigger('ems4');
	});
	$('#ems5').click(() => {
		mp.trigger('ems5');
	});
	$('#trs1').click(() => {
		mp.trigger('trs1');
	});
	$('#trs2').click(() => {
		mp.trigger('trs2');
	});
	$('#trs3').click(() => {
		mp.trigger('trs3');
	});
	$('#trs4').click(() => {
		mp.trigger('trs4');
	});

	$('#br1').click(() => {
		mp.trigger('br1');
	});
	$('#br2').click(() => {
		mp.trigger('br2');
	});
	$('#br3').click(() => {
		mp.trigger('br3');
	});
	$('#br4').click(() => {
		mp.trigger('br4');
	});

	$('#su1').click(() => {
		mp.trigger('su1');
	});
	$('#su2').click(() => {
		mp.trigger('su2');
	});
	$('#su3').click(() => {
		mp.trigger('su3');
	});
	$('#su4').click(() => {
		mp.trigger('su4');
	});
	$('#su5').click(() => {
		mp.trigger('su5');
	});


	$("#backButton").click(() => {
		$(".rozihide").slideUp(300, function () {
			$(".rozihide").hide();
			$(".mtitles").hide();
			$("#maint").show();
			$("#mainButtons").fadeIn(300);
			$("#backButton").fadeOut();
		});
	});


	//COLOR SLIDER
	var canvas = $("#myCanvas").get(0);
	var ctx = canvas.getContext("2d");
	var image = new Image();
	image.crossOrigin = 'anonymous';
	image.src = "https://i.imgur.com/WPrIoBg.png";
	image.onload = function () {
		ctx.drawImage(image, 0, 0);
	};
	$("#myCanvas").mousemove(function (e) { // mouse move handler

		var canvasOffset = $(canvas).offset();
		var canvasX = Math.floor(e.pageX - canvasOffset.left);
		var canvasY = Math.floor(e.pageY - canvasOffset.top);
		var imageData = ctx.getImageData(canvasX, canvasY, 1, 1);
		var pixel = imageData.data;
		var pixelColor = "rgba(" + pixel[0] + ", " + pixel[1] + ", " + pixel[2] + ", " + pixel[3] + ")";

		$('#preview').css('backgroundColor', pixelColor);

	});
	$('#myCanvas').click(function (e, red, green, bblue) { // mouse click handler
		var canvasOffset = $(canvas).offset();
		var canvasX = Math.floor(e.pageX - canvasOffset.left);
		var canvasY = Math.floor(e.pageY - canvasOffset.top);
		var imageData = ctx.getImageData(canvasX, canvasY, 1, 1);
		var pixel = imageData.data;
		var red = pixel[0];
		var green = pixel[1];
		var blue = pixel[2];
		mp.trigger('carColorChangeToServer', red, green, blue);

	});