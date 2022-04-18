/*	
	Name: average.js
	Date: #Insert the date here#
	Author: #Insert your name here#
*/

//	Using prompts, retrieves the name, hits, and at bats for the user
//	Passes these values to the battingAverage function
function load(){
	
	var name = prompt("Please enter the batter's full name.");
	var hits = prompt("How many hits did " + name + " achieve this year?");
	var abs = prompt("How many times has " + name + "  been up to bat this year?");
	
	battingAverage(name, hits, abs);

}

//	Calculates the user's average and rounds to 3 decimal places
//	Then the appropriate error message is displayed
function battingAverage(name, hits, abs) {
	//	Calculate the average

	var average = hits / abs;
	
	

	//	Format the result to 3 decimal places

	average = average.toFixed(3);

	//	Display the appropriate message inside the created H2 element
	//	Then add the H2 to the section
	var section = document.getElementById("results");
	var newH2 = document.createElement("h2");
	
	section.appendChild(newH2);	
	
	if (average < .300)
	{
	newH2.innerHTML = name + " is not exactly Cooperstown material with a batting average of " + average;
	}
	
	else 
	{
		newH2.innerHTML = "We're opening up the doors to Cooperstown for " + name + " and their batting average of " + average + "!";
	}


}


// 	Calls the load function when DOM has been completely loaded by the browser
document.addEventListener("DOMContentLoaded", load);