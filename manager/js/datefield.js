function addOnClickEvent(element, id) {
	var	dateChooserDiv = listener.getElement("jsdatechooser-" + id),
		inputChooser = listener.getElement(id);
	if(listener.referer.length && listener.referer[0] !== element) {
		listener.referer[0].datechooser.hide();
		listener.referer[0].showCalendar = false;
	}
	if(!element.showCalendar) {
		if(element.hasDateChooser)
			element.datechooser.redraw();
		else {
			element.datechooser = new JSDateChooser();
			element.datechooser.dayNames = ['D', 'S', 'T', 'Q', 'Q', 'S', 'S'];
			element.datechooser.monthNames = ['Janeiro', 'Fevereiro', 'Marco', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'];
			element.datechooser.addEventListener("change", listener);
			element.datechooser.draw("jsdatechooser-" + id);
			dateChooserDiv.style.position = "absolute";
			dateChooserDiv.style.left = Position.cumulativeOffset($(inputChooser))[0]; //((inputChooser.clientX || inputChooser.offsetLeft)+10) + "px";
			dateChooserDiv.style.top = Position.cumulativeOffset($(inputChooser))[1]+25;((inputChooser.clientY || inputChooser.offsetTop) + 37) + "px";
			element.hasDateChooser = true;
		};
		listener.referer = [element, id];
	}
	else
	element.datechooser.hide();
	element.showCalendar = !element.showCalendar;
};

var 	listener = new Object();
listener.referer = [];
listener.getElement = function(id) {
	return document.getElementById ? document.getElementById(id) : document.all[id];
};

listener.change = function(evt_obj) {
	var 	dt = evt_obj.target.selectedDate;
	this.referer[0].showCalendar = false;
	this.getElement(this.referer[1]).value = [
		dt.getDate() < 10 ? "0" + dt.getDate() : dt.getDate(),
		dt.getMonth() + 1 < 10 ? "0" + (dt.getMonth() + 1) : dt.getMonth() + 1,
		dt.getFullYear()		
	].join("/");
	evt_obj.target.hide();
	listener.referer = [];
};