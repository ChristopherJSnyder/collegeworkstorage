var itemDescription = ["MacBook", "The Razer", "WD My Passport", "Nexus 7", "DD-45 Drums"];
var itemPrice = [1899.99, 79.99, 179.99, 249.99, 119.99];
var itemImage = ["mac.png", "mouse.png", "wdehd.png", "nexus.png", "drums.png"];
var numberOfItemsInCart = 0;
var orderTotal = 0;


/*
 * Handles the submit event of the survey form
 *
 * param e  A reference to the event object
 * return   True if no validation errors; False if the form has
 *          validation errors
 */
function validate(e)
{
	
	formHasErrors();
	
	{	var result = false;
	//	Hides all error elements on the page
	hideErrors();

	//	Determine if the form has errors
	if(formHasErrors()){

		// 	Prevents the form from submitting
		
		e.preventDefault();
		// 	Returning false prevents the form from submitting
		result = false;
	}
	else {
		result = true;
	}

	return result;
	}
}

/*
 * Handles the reset event for the form.
 *
 * param e  A reference to the event object
 * return   True allows the reset to happen; False prevents
 *          the browser from resetting the form.
 */
function resetForm(e)
{
	// Confirm that the user wants to reset the form.
	if ( confirm('Clear order?') )
	{
		// Ensure all error fields are hidden
		hideErrors();
		
		// Set focus to the first text field on the page
		document.getElementById("qty1").focus();
		
		// When using onReset="resetForm()" in markup, returning true will allow
		// the form to reset
		return true;
	}

	// Prevents the form from resetting
	e.preventDefault();
	
	// When using onReset="resetForm()" in markup, returning false would prevent
	// the form from resetting
	return false;	
}

/*
 * Does all the error checking for the form.
 *
 * return   True if an error was found; False if no errors were found
 */
function formHasErrors()
{
	
	var errorFlag = false;

	var requiredTextFields 
		= ["fullname","address","city", "province", "postal", "email"];
	
	for (var i = 0; i < requiredTextFields.length; i++) {
		var textField = 
			document.getElementById(requiredTextFields[i]);
			
			
			ValidateEmail(email);

		if(!formFieldHasInput(textField)) {
			document
				.getElementById(requiredTextFields[i] + "_error")
				.style.display = "block";
				
				ValidateEmail(email);
				

			// only highlight and select the FIRST
			// error on the screen.
			if(!errorFlag) {
				textField.focus();
				textField.select();
			}
			// note that all other errors are still being 
			// shown, just not highlighted!

			errorFlag = true;
			return errorFlag;
		}
		
		
		
	}

	
	
	
}

/*
 * Adds an item to the cart and hides the quantity and add button for the product being ordered.
 *
 * param itemNumber The number used in the id of the quantity, item and remove button elements.
 */
function addItemToCart(itemNumber)
{
	// Get the value of the quantity field for the add button that was clicked 
	var quantityValue = trim(document.getElementById("qty" + itemNumber).value);

	// Determine if the quantity value is valid
	if ( !isNaN(quantityValue) && quantityValue != "" && quantityValue != null && quantityValue != 0 && !document.getElementById("cartItem" + itemNumber) )
	{
		// Hide the parent of the quantity field being evaluated
		document.getElementById("qty" + itemNumber).parentNode.style.visibility = "hidden";

		// Determine if there are no items in the car
		if ( numberOfItemsInCart == 0 )
		{
			// Hide the no items in cart list item 
			document.getElementById("noItems").style.display = "none";
		}

		// Create the image for the cart item
		var cartItemImage = document.createElement("img");
		cartItemImage.src = "images/" + itemImage[itemNumber - 1];
		cartItemImage.alt = itemDescription[itemNumber - 1];

		// Create the span element containing the item description
		var cartItemDescription = document.createElement("span");
		cartItemDescription.innerHTML = itemDescription[itemNumber - 1];

		// Create the span element containing the quanitity to order
		var cartItemQuanity = document.createElement("span");
		cartItemQuanity.innerHTML = quantityValue;

		// Calculate the subtotal of the item ordered
		var itemTotal = quantityValue * itemPrice[itemNumber - 1];

		// Create the span element containing the subtotal of the item ordered
		var cartItemTotal = document.createElement("span");
		cartItemTotal.innerHTML = formatCurrency(itemTotal);

		// Create the remove button for the cart item
		var cartItemRemoveButton = document.createElement("button");
		cartItemRemoveButton.setAttribute("id", "removeItem" + itemNumber);
		cartItemRemoveButton.setAttribute("type", "button");
		cartItemRemoveButton.innerHTML = "Remove";
		cartItemRemoveButton.addEventListener("click",
			// Annonymous function for the click event of a cart item remove button
			function()
			{
				// Removes the buttons grandparent (li) from the cart list
				this.parentNode.parentNode.removeChild(this.parentNode);

				// Deteremine the quantity field id for the item being removed from the cart by
				// getting the number at the end of the remove button's id
				var itemQuantityFieldId = "qty" + this.id.charAt(this.id.length - 1);

				// Get a reference to quanitity field of the item being removed form the cart
				var itemQuantityField = document.getElementById(itemQuantityFieldId);
				
				// Set the visibility of the quantity field's parent (div) to visible
				itemQuantityField.parentNode.style.visibility = "visible";

				// Initialize the quantity field value
				itemQuantityField.value = "";

				// Decrement the number of items in the cart
				numberOfItemsInCart--;

				// Decrement the order total
				orderTotal -= itemTotal;

				// Update the total purchase in the cart
				document.getElementById("cartTotal").innerHTML = formatCurrency(orderTotal);

				// Determine if there are no items in the car
				if ( numberOfItemsInCart == 0 )
				{
					// Show the no items in cart list item 
					document.getElementById("noItems").style.display = "block";
				}				
			},
			false
		);

		// Create a div used to clear the floats
		var cartClearDiv = document.createElement("div");
		cartClearDiv.setAttribute("class", "clear");

		// Create the paragraph which contains the cart item summary elements
		var cartItemParagraph = document.createElement("p");
		cartItemParagraph.appendChild(cartItemImage);
		cartItemParagraph.appendChild(cartItemDescription);
		cartItemParagraph.appendChild(document.createElement("br"));
		cartItemParagraph.appendChild(document.createTextNode("Quantity: "));
		cartItemParagraph.appendChild(cartItemQuanity);
		cartItemParagraph.appendChild(document.createElement("br"));
		cartItemParagraph.appendChild(document.createTextNode("Total: "));
		cartItemParagraph.appendChild(cartItemTotal);		

		// Create the cart list item and add the elements within it
		var cartItem = document.createElement("li");
		cartItem.setAttribute("id", "cartItem" + itemNumber);
		cartItem.appendChild(cartItemParagraph);
		cartItem.appendChild(cartItemRemoveButton);
		cartItem.appendChild(cartClearDiv);

		// Add the cart list item to the top of the list
		var cart = document.getElementById("cart");
		cart.insertBefore(cartItem, cart.childNodes[0]);

		// Increment the number of items in the cart
		numberOfItemsInCart++;

		// Increment the total purchase amount
		orderTotal += itemTotal;

		// Update the total puchase amount in the cart
		document.getElementById("cartTotal").innerHTML = formatCurrency(orderTotal);
	}		
}

/*
 * Hides all of the error elements.
 */
function hideErrors()
{
		var errorFields = document.getElementsByClassName("cardTypeError error");

	for (var i = 0; i < errorFields.length; i++) 
	{
		errorFields[i].style.display = "none";
	}
	
	
		var errorFields = document.getElementsByClassName("cardError error");

	for (var i = 0; i < errorFields.length; i++) 
	{
		errorFields[i].style.display = "none";
	}
	
		var errorFields = document.getElementsByClassName("shippingError error");

	for (var i = 0; i < errorFields.length; i++) 
	{
		errorFields[i].style.display = "none";
	}

	
			var errorFields = document.getElementsByClassName("shippingError error");

	for (var i = 0; i < errorFields.length; i++) 
	{
		errorFields[i].style.display = "none";
	}


}

function formFieldHasInput(fieldElement)
{
	return fieldElement.value 
		&& trim(fieldElement.value);

	// // Check if the text field has a value
	// if ( fieldElement.value == null || trim(fieldElement.value) == "" )
	// {
	// 	// Invalid entry
	// 	return false;
	// }
	
	// // Valid entry
	// return true;
}

    function ValidateEmail(mail)   
    {  
     if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(myForm.emailAddr.value))  
      {  
        return (true)  
      }  
       
        return (false)  
    }  






/*
 * Handles the load event of the document.
 */
function load()
{
	hideErrors();
	
	document.getElementById("orderform").submit;
	document.getElementById("orderform").addEventListener("submit", validate);
	
	
	document.getElementById("orderform").reset;
	document.getElementById("orderform").addEventListener("reset", resetForm);
	
	document.getElementById("addMac").addEventListener("click", function () { addItemToCart(1) });
	document.getElementById("addMouse").addEventListener("click", function () { addItemToCart(2) });
	document.getElementById("addWD").addEventListener("click", function () { addItemToCart(3) });
	document.getElementById("addNexus").addEventListener("click", function () { addItemToCart(4) });
	document.getElementById("addDrums").addEventListener("click", function () { addItemToCart(5) });
	

}

// Add document load event listener
document.addEventListener("DOMContentLoaded", load, false);