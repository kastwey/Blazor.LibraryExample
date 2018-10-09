// This file provides javascript functions that can be used from c# code in a blazor component.

window.interopJsFunctions = {
	showPrompt: function (message) {
		return prompt(message, 'Type anything here');
	},
	focus: function (element) {
		if (element === null || element === undefined) {
			return false;
		}

		var attributeValue = element.getAttribute("tabindex");
		if (attributeValue === null || attributeValue === "" || attributeValue === "0") {
			element.setAttribute("tabindex", "-1");
		}
		return element.focus();
	},
	alert: function (text) {
		alert(text);
	},
	focusByQuerySelector: function (selector) {
		var element = document.querySelector(selector);
		if (element !== null) {
			var attributeValue = element.getAttribute("tabindex");
			if (attributeValue === null || attributeValue === "" || attributeValue === "0") {
				element.setAttribute("tabindex", "-1");
			}

			return element.focus();
		}
		return false;
	},
	focusInHeader: function (level) {
		var element = document.querySelector("h" + level);
		if (element !== null) {
			var attributeValue = element.getAttribute("tabindex");
			if (attributeValue === null || attributeValue === "" || attributeValue === "0") {
				element.setAttribute("tabindex", "-1");
			}
			element.focus();
		}
	},
	click: function (element) {
		element.click();
	},
	setTitle: function (title) {
		document.title = title;
	},
	getTitle: function () {
		return document.title;
	},
	confirm: function (message) {
		return confirm(message);
	}
};
