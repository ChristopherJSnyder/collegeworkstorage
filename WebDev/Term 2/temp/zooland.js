/*
    Script name: Zooland.js
    Description: Used to display animals and related info from an XML file
    Author: 
    Date Created:
*/

function load() {

  // Code that you want executed once the DOM has loaded goes here.

  var zooland_xml = loadXML('zooland.xml');

  // Let's do a bit of sanity checking:

  // Ensure that the xml has loaded correctly.
  console.log(xmlToString(zooland_xml));

  // Find the name of the first animal and write it to the console.
  var first_animal = zooland_xml.getElementsByTagName('animal')[0];
  var common_name_element = first_animal.getElementsByTagName('common_name')[0];
  var common_name = common_name_element.firstChild.nodeValue;
  console.log('The common name of the first animal is: ' + common_name);

}

// Other event listeners can go here.
document.addEventListener("DOMContentLoaded", load);