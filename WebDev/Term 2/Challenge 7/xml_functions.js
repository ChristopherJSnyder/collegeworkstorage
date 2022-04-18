// A cross-browser "To String" helper for xml node objects.
function xmlToString(node) {
	if (node.xml) { // Only IE supports this property.
		return node.xml;
	} else if (XMLSerializer) { // Firefox supports this.
		var my_serializer = new XMLSerializer();
		return my_serializer.serializeToString(node);
	} else {
		alert('Your browser does not support XML serialization.');
		return "";
	}
}

//synchronously loads the passed XML document as a DOM Document object, and returns it
function loadXML(filename) {
	if (window.XMLHttpRequest)
	  {
	  xhttp=new XMLHttpRequest();
	  }
	else //for IE5 and IE6 holdouts
	  {
	  xhttp=new ActiveXObject("Microsoft.XMLHTTP");
	  }
	xhttp.open("GET", filename, false);
	xhttp.send();
	return xhttp.responseXML;
}