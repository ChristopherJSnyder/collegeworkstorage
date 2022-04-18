function load(){
	
	var submit = document.getElementsByTagName("button")[0];
	submit.addEventListener("click", calcData);
	loadDataFromCache();
}

function calcData()
{
	var name = document.getElementById("fullName").value;
	var hours = document.getElementById("hoursWorked").value;
	var payrate = document.getElementById("hourlyRate").value;
	
	
	var overtimehours = parseFloat(overtimehours);
	var overtimepay = parseFloat(overtimepay);
	var grosspay = parseFloat(grosspay);
	var netpay = parseFloat(netpay);
	var tax = parseFloat(tax);
	var hours = parseFloat(hours);
	var payrate = parseFloat(payrate);
	var regularhours = parseFloat(regularhours);
	
	// Validate data before proceeding
	if (hours <= 0 || payrate <= 0 || isNaN(hours) || isNaN(payrate))
	{
		alert("Invalid Data! You can't work for negative hours or get paid negative dollars per hour, dingus!");
	}
	
	else {
	
	// Calculate overtime pay. If hasn't worked more than 40 hours, will default to zero.
	overtimehours = hours - 40;
	if (overtimehours <= 0)
	{
		overtimehours = 0;
	}
	
	else 
	{
		overtimepay = overtimehours * (payrate * 1.50);
		
	}
	
	// Regular hours is a standard 40 hour workweek. Checks against overtime hours to determine if all 40 were worked or less. 
	
	hours = hours - overtimehours;
	
	var regularpay = hours * payrate;
	
	
	// Calculate gross pay (before taxes) by calculating standard and overtime pay where applicabpe. 
	grosspay = overtimepay + regularpay;
	
	
	// Determine the tax rate by comparing gross pay to a few different tax brackets.
	var taxrate;
	var taxmoney;
	
	if (grosspay < 250)
	{
		taxrate = 0.25
	}
	
	else if (grosspay >= 250 && grosspay <500)
	{
		taxrate = 0.30;
	}
	
	else if (grosspay >= 500 && grosspay <750)
	{
		taxrate = 0.40;
	}
	
	else 
	{
		taxrate = 0.50;
	}
	
	
	// Calculate how much money is taken off by the taxes and thus the net pay. Both these variables will be placed in the table.
	taxmoney = grosspay * taxrate
	netpay = grosspay - taxmoney;
	
	
	grosspay = '$' + grosspay.toFixed(2);
	taxmoney = '$' + taxmoney.toFixed(2);
	netpay = '$' + netpay.toFixed(2);
	
	
		displayData(name, grosspay, taxmoney, netpay);
		
	}
}


function displayData(name, grosspay, taxmoney, netpay)
{
	
	var tbody = document.getElementsByTagName("tbody")[0];
	var newTr = document.createElement("tr");
	var newNameTd = document.createElement("td");
	var newGrossPayTd = document.createElement("td");
	var newTaxTd = document.createElement("td");
	var newNetPayTd = document.createElement("td");
	
	newNameTd.innerHTML = name;
	newGrossPayTd.innerHTML = grosspay;
	newTaxTd.innerHTML = taxmoney;
	newNetPayTd.innerHTML = netpay;
	
	newTr.appendChild(newNameTd);
	newTr.appendChild(newGrossPayTd);
	newTr.appendChild(newTaxTd);
	newTr.appendChild(newNetPayTd);
	
	tbody.appendChild(newTr);
	
	
	saveDataToCache();
	resetFields();
	
	
	
}

// Reset fields in form.

function resetFields() {
	document.getElementById("fullName").value = "";
	document.getElementById("hoursWorked").value = "";
	document.getElementById("hourlyRate").value = "";
	document.getElementById("name").focus();
}

// Save the data to cache.

function saveDataToCache() {
	var tbody = document.getElementById("employees");
	localStorage.setItem("employeesData",tbody.innerHTML);
}

// Load data from cache if any exists.

function loadDataFromCache() {
	if(localStorage.getItem("employeesData")) {
		var employees = document.getElementById("employees");
		employees.innerHTML = localStorage.getItem("employeesData");
	}
}


document.addEventListener("DOMContentLoaded",load, false);