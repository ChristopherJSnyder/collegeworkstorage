/*
    Script name: Zooland.js
    Description: Used to display animals and related info from an XML file
    Author: 
    Date Created:
*/


function showAll()
{  
	// Load XML document and grab its animals
	var zooland_xml = loadXML('zooland.xml');
	var animals = zooland_xml.getElementsByTagName('animal');
	
	// Make the item that the animals will go into
	var newUl = document.getElementById("animals");
	for (var i = 0; i < animals.length; i++)
	{
		
		// Current animal is next one up on list of animals in XML document
	  
		var current_animal = animals[i];
  
		// Grab common name, element, description and their values
		
		var common_name_element = current_animal.getElementsByTagName('common_name')[0];
		var common_name = common_name_element.firstChild.nodeValue;
		var scientific_name_element = current_animal.getElementsByTagName('scientific_name')[0];
		var scientific_name = scientific_name_element.firstChild.nodeValue;
		var description_element = current_animal.getElementsByTagName('description')[0];
		var description = description_element.firstChild.nodeValue;

		
		// Create and add respective elements with their formatting to the HTML
		
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

		// Use loop to get total amount of images for this animal and add them all to the HTML
		
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
	}
}


function searchAnimals()
{
	
	// Functions the exact same as show all animals except as explained below
	
	var zooland_xml = loadXML('zooland.xml');
	var animals = zooland_xml.getElementsByTagName('animal');
	var searchText = document.getElementById("search_text").value;
	var animalsText = document.getElementById("animals");
	
	var newUl = document.getElementById("animals");
	
	for (var i = 0; i < animals.length; i++)
	{
		
	  
		var current_animal = animals[i];

		var common_name_element = current_animal.getElementsByTagName('common_name')[0];
		var common_name = common_name_element.firstChild.nodeValue;
		var scientific_name_element = current_animal.getElementsByTagName('scientific_name')[0];
		var scientific_name = scientific_name_element.firstChild.nodeValue;
		
		// Look to see if searchtext has common or scientific name and only print an animal if it does (user can search for multiple animals at once)
		
		
		if (common_name.includes(searchText) || scientific_name.includes(searchText))
		{
			alert ("test1");
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
			  alert("test3");
			  var newImg = document.createElement("IMG")
			  var currentImage = images[r];
			  var imageA = currentImage.firstChild.nodeValue;
			  
			  newImg.src = "images/" + imageA;
			  
			  newLi.appendChild(newImg);
		  }
		   
		newUl.appendChild(newLi);
		}
		
		else
		{
			animalsText.innerHTML.value = "<li>" + searchText + " not found. </li>";
		}
			
		
	}
}




