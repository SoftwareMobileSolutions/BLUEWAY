// ---------Responsive-navbar-active-animation-----------
function menuAction() {
	var tabsNewAnim = $('#navbarSupportedContent');
	var selectorNewAnim = $('#navbarSupportedContent').find('li').length;
	var activeItemNewAnim = tabsNewAnim.find('.active');
	var activeWidthNewAnimHeight = activeItemNewAnim.innerHeight();
	var activeWidthNewAnimWidth = activeItemNewAnim.innerWidth();
	var itemPosNewAnimTop = activeItemNewAnim.position();
	var itemPosNewAnimLeft = activeItemNewAnim.position();
	$(".hori-selector").css({
		"top": itemPosNewAnimTop.top + "px",
		"left": itemPosNewAnimLeft.left + "px",
		"height": activeWidthNewAnimHeight + "px",
		"width": activeWidthNewAnimWidth + "px"
	});
	$("#navbarSupportedContent").on("click", "li", function (e) {
		$('#navbarSupportedContent ul li').removeClass("active");
		$(this).addClass('active');
		var activeWidthNewAnimHeight = $(this).innerHeight();
		var activeWidthNewAnimWidth = $(this).innerWidth();
		var itemPosNewAnimTop = $(this).position();
		var itemPosNewAnimLeft = $(this).position();
		$(".hori-selector").css({
			"top": itemPosNewAnimTop.top + "px",
			"left": itemPosNewAnimLeft.left + "px",
			"height": activeWidthNewAnimHeight + "px",
			"width": activeWidthNewAnimWidth + "px"
		});
	});
}
$(document).ready(function () {
	setTimeout(function () { menuAction(); });
});
$(window).on('resize', function () {
	setTimeout(function () { menuAction(); }, 500);
});
$(".navbar-toggler").click(function () {
	$(".navbar-collapse").slideToggle(300);
	setTimeout(function () { menuAction(); });
});


function createMenu() {
	var urlPageDefault = "";
	var strHTML = "";
	var menu = [
		{
			title: "Solicitar Viajes",
			icon: "fas fa-tachometer-alt",
			url: "adSolicitarViaje/adSolicitarViaje",
			activo: true,
			onclick: ""
		},
		/*{
			title: "Asignaci\xF3n de Viaje",
			url: "adAsignarVehiculoAViaje/adAsignarVehiculoAViaje",
			icon: "far fa-clone",
			activo: false,
			onclick: ""
		},*/
		{
			title: "Salir",
			url: "Login/CerrarSesion",
			icon: "far fa-exit",
			activo: false,
			onclick: "$.get('" + pathRequest  +"Login/CerrarSesion', {}, function() { location.reload(); })"
		}
	];
	strHTML += 
	`
	<div class="hori-selector">
		<div class="left"></div>
		<div class="right"></div>
	</div>
	`;
	for (var i = 0, len = menu.length; i < len; i++) {
		menu[i].activo && (urlPageDefault = menu[i]["url"]);
		strHTML+=
		`
		<li class="nav-item ${menu[i]["activo"] ? "active" : ""}" onclick="${menu[i]["onclick"] === "" ? "getPage('" +  menu[i]["url"] + "')" : menu[i]["onclick"]   }">
			<a class="nav-link" href="javascript:void(0);"><i class="${menu[i]["url"]}"></i>${menu[i]["title"]}</a>
		</li>
		`;
	}
	$("#menuMain").html(strHTML);
	//console.log(urlPageDefault);
	getPage(urlPageDefault);
}

function getPage(url) {
	$.get(pathRequest + url, {}, function (d) {
		$("#contentMain").html(d);
	});
}

createMenu();

// --------------add active class-on another-page move----------
/*
 jQuery(document).ready(function ($) {
	// Get current path and find target link
	var path = window.location.pathname.split("/").pop();

	// Account for home page with empty path
	if (path == '') {
		path = 'index.html';
	}

	var target = $('#navbarSupportedContent ul li a[href="' + path + '"]');
	// Add active class to target link
	target.parent().addClass('active');
});
*/



// Add active class on another page linked
// ==========================================
// $(window).on('load',function () {
//     var current = location.pathname;
//     console.log(current);
//     $('#navbarSupportedContent ul li a').each(function(){
//         var $this = $(this);
//         // if the current path is like this link, make it active
//         if($this.attr('href').indexOf(current) !== -1){
//             $this.parent().addClass('active');
//             $this.parents('.menu-submenu').addClass('show-dropdown');
//             $this.parents('.menu-submenu').parent().addClass('active');
//         }else{
//             $this.parent().removeClass('active');
//         }
//     })
// });