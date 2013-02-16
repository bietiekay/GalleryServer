/* Day Ajax GalleryServer Handling */

function DayGalleryDataReceived(series)
{
	for (var key in series)
	{
		if (series.hasOwnProperty(key))
		{
			var gallery = document.getElementById("gallery");
			
			var newA = document.createElement("A");
			newA.href = series[key];
			newA.target = "new";
			newA.innerHTML = "<img src='"+series[key]+"/thumbnail'>";
			gallery.appendChild(newA);
		}
	}
}


function ReceiveDayGallery(dayGalleryURL)
{
	// clear the gallery...
	var gallery = document.getElementById("gallery");
	if ( gallery.hasChildNodes() )
	{
	    while ( gallery.childNodes.length >= 1 )
	    {
	        gallery.removeChild( gallery.firstChild );       
	    } 
    }


	
	$.ajax(
	{
		url: dayGalleryURL,
		method: 'GET',
		dataType: 'json',
		success: function(series) {	DayGalleryDataReceived(series); }
	});	
	
}

function DayDataReceived(series)
{
	for (var key in series)
	{
		if (series.hasOwnProperty(key)) 
		{
			//alert(series[key]);
			var menu = document.getElementById("menu");
			var newLI = document.createElement("LI");
			newLI.innerHTML = "<a href=\"#\" onclick=\"ReceiveDayGallery('"+series[key]+"')\">"+
								key+
								"</a>";

			menu.appendChild(newLI);
			
		}
	}
}

function ReceiveDayData(dayURL)
{
	$.ajax(
	{
		url: dayURL,
		method: 'GET',
		dataType: 'json',
		success: function(series) {	DayDataReceived(series); }
	});	
}