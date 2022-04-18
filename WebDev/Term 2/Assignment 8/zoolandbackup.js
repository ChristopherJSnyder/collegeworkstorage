/*
    Script name: Zooland.js
    Description: Used to display animals and related info from an XML file
    Author: 
    Date Created:
*/


function showAll()
{  
	var zooland_xml = loadXML('zooland.xml');
	var animals = zooland_xml.getElementsByTagName('animal');
	
	var newUl = document.getElementById("animals");
	for (var i = 0; i < animals.length; i++)
	{
	  
		var current_animal = animals[i];
  
		var common_name_element = current_animal.getElementsByTagName('common_name')[0];
		var common_name = common_name_element.firstChild.nodeValue;
		var scientific_name_element = current_animal.getElementsByTagName('scientific_name')[0];
		var scientific_name = scientific_name_element.firstChild.nodeValue;
		var description_element = current_animal.getElementsByTagName('description')[0];
		var description = description_element.firstChild.nodeValue;

		var newh3 = document.createElement("h3");
		newh3.innerHTML = common_name;

		var newh4 = document.createElement("h4");
		newh4.innerHTML = scientific_name;

		var newBlock = document.createElement("blockquote");
		newBlock.innerHTML = description;

		var contents = document.getElementById("animals");

		var newLi = document.createElement("li");
		newLi.appendChild(newh3);
		newLi.appendChild(newh4);
		newLi.appendChild(newBlock);

		

		var images = current_animal.getElementsByTagName('image');

		  for (var r = 0; r < images.length; r++)
		  {
			  var newImg = document.createElement("IMG")
			  var currentImage = images[r];
			  var imageA = currentImage.firstChild.nodeValue;
			  
			  newImg.src = "images/" + imageA;
			  
			  newLi.appendChild(newImg);
			  
		  }
		   
		newUl.appendChild(newLi);
  
		//var results = document.getElementById("result");
		//results.innerHTML = "";

	}
	

}


// Other event listeners can go here.


