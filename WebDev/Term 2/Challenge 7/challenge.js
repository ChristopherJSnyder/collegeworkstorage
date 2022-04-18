/*
	This (fairly) simple Javascript file will load 2 xml files.
	Elements will be copied from the Canada Data file, and then
	will be added to the Country Data file under a new Canada
	element. 

	Care must be taken to ensure the addition of only the appropriate
	elements from the Canada Data file, AND that they are placed correctly
	in the Country Data file, according to specifications.
*/

function challenge(){

	// load the xml doms
	var countryXML = loadXML("CountryData.xml");
	var canadaXML = loadXML("CanadaData.xml");

	// obtain the root element in order to insert elements
	// in the appropriate places
	var countryRoot = countryXML.documentElement;
	var canadaRoot = canadaXML.getElementsByTagName("CanadaData")[0];

	// show both xml doms (you can comment these out after you're sure
	// the data has been loaded correctly)
	

	/* The sequence of events:
		- Create a new country node for Canada in the countryXML 
		  and insert it in the Country DOM
		- Get the output div from the HTML DOM, display appropriate 
		  messages throughout the script, according to specs
		- COPY appropriate nodes from Canada DOM, and add them as children
		  of the new Canada element
		- Output the updated Country DOM to the console
		- Syntax example of the insertBefore method:
			xmlRoot.insertBefore(newElement, elementYouWantToInsertItBefore);
	*/
	
	
	 var newCountry = countryXML.createElement("Canada");
countryRoot.insertBefore(newCountry, countryRoot.getElementsByTagName("Angola") [0]);

	//var dateSurveyElement = canadaRoot.getElementsByTagName("DateOfSurvey")[0];
	//var dateSurvey = dateSurveyElement.firstChild.nodeValue;
	//alert(dateSurvey);
	
	//var date_node = canadaRoot.getElementsByTagName('DateOfSurvey')[0];
	//var datecloned_node = date_node.cloneNode(true);
	//newCountry.appendChild(datecloned_node);

	var gdp_node = canadaRoot.getElementsByTagName('GDP')[0];
	var gdpcloned_node = gdp_node.cloneNode(true);
	newCountry.appendChild(gdpcloned_node);

	//var exports_node = canadaRoot.getElementsByTagName("Exports")[0];
	//var exportscloned_node = exports_node.cloneNode(true);
	//newCountry.appendChild(exportscloned_node);

	//var imports_node = canadaRoot.getElementsByTagName("Imports")[0];
	//var importscloned_node = imports_node.cloneNode(true);
	//newCountry.appendChild(importscloned_node);


	//var electricityproduction_node = canadaRoot.getElementsByTagName("ElectricityProduction")[0];
	//var electricityproductioncloned_node = electricityproduction_node.cloneNode(true);
	//newCountry.appendChild(electricityproductioncloned_node);

	//var electricityconsumption_node = canadaRoot.getElementsByTagName("ElectricityConsumption")[0];
	//var electricityconsumptioncloned_node = electricityconsumption_node.cloneNode(true);
	//newCountry.appendChild(electricityconsumptioncloned_node);

	//var elecex_node = canadaRoot.getElementsByTagName("ElectricityExports")[0];
	//var elecexcloned_node = elecex_node.cloneNode(true);
	//newCountry.appendChild(elecexcloned_node);

	//var birth_node = canadaRoot.getElementsByTagName("BirthRate")[0];
	//var birthcloned_node = birth_node.cloneNode(true);
	//newCountry.appendChild(birthcloned_node);

	var pop_node = canadaRoot.getElementsByTagName("population")[0];
	var popcloned_node = pop_node.cloneNode(true);
	newCountry.appendChild(popcloned_node);

	var lifeexpect_node = canadaRoot.getElementsByTagName("LifeExpectancy")[0];
	var lifeexpectcloned_node = lifeexpect_node.cloneNode(true);
	newCountry.appendChild(lifeexpectcloned_node);


	//var popgrow_node = canadaRoot.getElementsByTagName("PopulationGrowthRate")[0];
	//var popgrowcloned_node = popgrow_node.cloneNode(true);
	//newCountry.appendChild(popgrowcloned_node);

	//alert(xmlToString(newCountry));
	alert(xmlToString(countryXML));
	//alert(xmlToString(canadaXML));



}

//	To be run when the browser has loaded the DOM
document.addEventListener("DOMContentLoaded",challenge,false);