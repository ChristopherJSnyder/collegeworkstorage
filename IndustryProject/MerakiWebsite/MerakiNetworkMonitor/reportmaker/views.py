from django.shortcuts import render
from .models import *
from django.conf import settings
from django.core.files.storage import FileSystemStorage
from .forms import *
from django.http import HttpResponse, HttpResponseRedirect
from datetime import datetime, timedelta
from django.utils import timezone
from django.views.decorators.csrf import csrf_exempt
from django.utils.decorators import method_decorator


# Create your views here.

def homepage(request):
	
	
	return render(request, 'homepage.html')
		
	
def choosereportinput(request):
	
	return render(request, 'choosereportinput.html')
	
	
def addreports(request):

	'''If user has submitted form, grab the form data and to the view for handling new reports'. If not, render entry form.'''
	if request.method == "POST":
		form = RawForm(request.POST)
		if form.is_valid():
			raw = form.data['raw']
			return HttpResponseRedirect('handlereports', {'raw' : raw})
			
		
	
	else:	
		form = RawForm
		return render(request, 'addreports.html', {'form':form})
		

		
def addreportsfile(request):

	'''If user has submitted form, grab the form data and to the view for handling new reports, if not render file upload form'''
	if request.method == "POST":
		form = ReportUploadForm(request.POST)
		if form._is_valid():
			file = form.data['file']
			return HttpResponseRedirect('addreportsfile', {'file' : file})

	form = ReportUploadForm
	return render(request, 'addreportsfile.html', {'form' : form})

	
def deletereports(request):

	
	return render(request, 'deletereports.html')
	
def handlereports(request):

	'''If user is trying to get to this page by entering it into the address bar and not by submitting a form, redirect to entry page'''
	if request.method == "GET":
		form = RawForm
		return HttpResponseRedirect('addreports', {'form' : form})
		
	
	else:
	
		'''Get all of the raw pasted data and split into a list, with each list item being one report to handle'''
		testraw = request.POST.get('raw')
		reports = Report.objects.all()
		testraw = testraw.split("\n")
		
		for line in testraw:
		
			'''Use functions defined in Models file to fetch all the necessary data from the report'''
			mac = getMAC(line)
			date = getDate(line)
			site = getSite(line)
			requesttype = getRequestType(line)
			source = getSourceIP(line)
			destination = getDestinationIP(line)
			device = getDevice(line)
			upload = getUpload(line)
			download = getDownload(line)
			
			'''Create a new Report object and save it'''
			report = Report(mac = mac, date = date, site = site, source = source, destination = destination, device = device, download = download, upload = upload)
			report.save()
			
			try:
			
				'''Try to create a new mac nickname for the MAC in this entry with the default nickname. If there is an exception, it is likely because the mac in the report already exists in the database.'''
				macname = Macname(mac = mac, nickname = "No nickname associated")
				macname.save()
			
			except:
				pass
				
			
		
		return render (request, 'handlereports.html', {'reports' : reports, 'raw' : testraw,})
	
	
	
def handlereportsfile(request):

	'''Get uploaded text file from previous page'''
	file = request.FILES['file']
	
	'''Assign the file's contents to a variable, change it to a string, and format it a bit, splitting each entry into a list item'''
	'''An errant 'b' is added to the first line of the file which mucks up grabbing the date, so removing it is the only way I've found to fix it'''
	content = file.read()
	content = str(content)
	content = content.replace("b'", "")
	content = content.split("\\r\\n")
	
	
	
	

	'''Number of successful and rejected entries, to be displayed to the user'''
	good = 0
	bad = 0


	for line in content:
	
		try:
		
			'''Grab all needed data from the entry. If an entry returns an exception, it is rejected and the counter for it increases.'''
			mac = getMAC(line)
			date = getDate(line)
			site = getSite(line)
			requesttype = getRequestType(line)
			source = getSourceIP(line)
			destination = getDestinationIP(line)
			device = getDevice(line)
			upload = getUpload(line)
			download = getDownload(line)
				
			report = Report(mac = mac, date = date, site = site, source = source, destination = destination, device = device, download = download, upload = upload)
			report.save()
			good = good + 1
		except:
			pass
			bad = bad + 1
		
		
		try:
	
			'''Try to create a new mac nickname for the MAC in this entry with the default nickname. If there is an exception, it is likely because the mac in the report already exists in the database.'''
			macname = Macname(mac = mac, nickname = "No nickname associated")
			macname.save()
		
		except:
			pass
	
		
	
		
	
	return render (request, 'handlereportsfile.html', {'good': good, 'bad' : bad})
	

	
def handledeletereports(request):

	'''Super simple. Since the option is for clearing up the database, simply grab every Report and delete it. Don't touch the nicknames though.'''
	reports = Report.objects.all()
	reports.delete()
	return render (request, 'homepage.html')

	
def viewreports(request):
	
	return render(request, 'viewreports.html')
	
	

def bymac(request):

	'''Get given mac, number of entries, and within how many days, hand them over to next view'''
	if request.method == "POST":
		form = MACInputForm(request.POST)
		if form.is_valid():
			mac = form.data['mactofind']
			days = form.data['days']
			entries = form.data['entries']
			return HttpResponseRedirect('handlemac', {'mac' : mac, 'days' : days})
		
	
	else:	
		form = MACInputForm
		return render(request, 'bymac.html', {'form':form})
			


def handlemac (request):

	'''Redirect to previous page if the user got here via the address bar and not by submitting the form'''
	if request.method == "GET":
		form = MACInputForm
		return HttpResponseRedirect('bymac', {'form' : form})
		
	else:
		'''Get the search terms'''
		mac = request.POST.get('mactofind')
		entries = (request.POST.get('entries'))
		days = request.POST.get('days')
		
		'''If the number of entries field is blank, then the user has indicated there is to be no limit on days. Thus, assign to number of entries to something super high'''
		if entries == "":
			entries = 999999
			
		else:
			entries = int(entries)
			
			
		'''Get the date to search as far back as'''
		days = int(days)
		daysago = timezone.now() - timedelta(days=days)
		date = timezone.now()
		
		'''Get all reports that match the criteria'''
		reports = Report.objects.filter(date__range=(daysago, date), mac=mac).order_by('-date')[0:entries]
		
		if not reports.exists():
			try:
				'''If there are no entries for the given MAC address, check to see if it is a MAC Nickname instead and display reports for it. If not, then return no results.'''
				newmac = Macname.objects.values_list('mac', flat = True).filter(nickname=mac)[0]
				reports = Report.objects.filter(date__range=(daysago, date), mac=newmac).order_by('-date')[0:entries]
				
			except:
				reports = ()

		
		try:
			nickname = Macname.objects.filter(mac=mac).values_list('nickname', flat=True)[0]
			
		except:
			nickname = "None associated"
		

		return render (request, 'handlemac.html', {'reports' : reports, 'mac' : mac, 'nickname' : nickname, 'entries' : entries, 'days' : days})
	
	
	
def macnamesview (request):
	'''Return all MACs and their nicknames'''
	macnames = Macname.objects.all()
	return render (request, 'macnamesview.html', {'macnames' : macnames})
	
	

def byaccess (request):
	
	'''Get search query from page and pass to next view...'''
	if request.method == "POST":
		form = AccessInputForm(request.POST)
		if form.is_valid():
			device = form.data['access']
			entries = form.data['entries']
			days = form.data['days']
			return HttpResponseRedirect('handleaccess', {'device' : device, 'entries' : entries,})
		
	
	else:	
		form = AccessInputForm
		return render(request, 'byaccess.html', {'form':form})
		
		
		
def handleaccess (request):
	
	'''Redirect to prior page if the user didnt get here by submitting a form'''
	if request.method == "GET":
		form = AccessInputForm
		return HttpResponseRedirect('byaccess', {'form' : form})
		
	else:
	
		'''Get form data'''
		device = request.POST.get('access')
		entries = request.POST.get('entries')
		days = request.POST.get('days')
		
		'''The user was instructed to keep the field blank if there was to be no entry limit, thusly make the limit very high'''
		if entries == "":
			entries = 999999
			
		else:
			entries = int(entries)
			
			
		'''Figure out how far to look back depending on how many past days the user gave'''
		days = int(days)
		daysago = timezone.now() - timedelta(days=days)
		date = timezone.now()
		
		'''Retrieve all reports that fit the search query.'''
		reports = Report.objects.filter(date__range=(daysago, date),device=device).order_by('-date')[0:entries]
		
		for report in reports:
			
			mactolookfor = report.mac
			
			
			nickname = Macname.objects.filter(mac=mactolookfor).values_list('nickname', flat=True)[0]
			
			if nickname != "":
				report.mac = nickname
				
			else:
				pass
		
		return render (request, 'handleaccess.html', {'reports': reports, 'device' : device, 'entries' : entries, 'days' : days})
	
	
	
def bysite (request):

	'''Gather form data and pass on to next view'''
	if request.method == "POST":
		form = SiteInputForm(request.POST)
		if form.is_valid():
			mac = form.data['mactofind']
			entries = form.data['entries']
			days = form.data['days']
			return HttpResponseRedirect('handlesite', {'mac' : mac, 'entries' : entries,})
		
	
	else:	
		form = SiteInputForm
		return render (request, 'bysite.html', {'form' : form})
		
		
		
def handlesite (request):
	
	'''If the user didnt get here by submitting a form, send back to prior page'''
	if request.method == "GET":
		form = SiteInputForm
		return HttpResponseRedirect('bysite', {'form' : form})
		
	else:
		mac = request.POST.get('mactofind')
		days = request.POST.get('days')
		
		
			
		
		
		'''Find out how many days to look back'''
		days = int(days)
		daysago = timezone.now() - timedelta(days=days)
		date = timezone.now()
		
		'''Find all reports that match search query'''
		reports = Report.objects.values_list('site', flat=True).filter(date__range=(daysago, date), mac=mac)
		
		
		'''If the given mac has no matches, try to see if it exists on the Macnames list, and if so display the results for that. Else, provide an empty list.'''
		if not reports.exists():
			try:
				newmac = Macname.objects.values_list('mac', flat = True).filter(nickname=mac)[0]
				reports = reports = Report.objects.values_list('site', flat=True).filter(date__range=(daysago, date), mac=newmac)
				
			except:
				reports = ()
		
		try:
			nickname = Macname.objects.values_list('nickname', flat = True).filter(mac=mac)[0]
		except:
			nickname = "None associated"
		
		
		'''Create list of sites for use in loop...'''
		listofsites = {}
		
		'''Loop through all retrieved reports. 
		Each time a unique website visit appears, add it to the list. If it appears again, increment the amount of times visited. 
		Deliver sites and number of times they've been visited to user.'''
		
		for site in reports:
			if site == "None":
				pass
		
			elif site not in listofsites:
				listofsites[site] = 1
				
				
			else: 
				listofsites[site] += 1
				
		
		return render (request, 'handlesite.html', {'mac' : mac, 'nickname' : nickname, 'days' : days, 'listofsites' : sorted(listofsites.items())})
	
	
	
def bydownloadupload (request):

	''''If posting the form, grab the mac to look for, how many entries, and how many days back, send to next view'''
	if request.method == "POST":
		form = DownloadUploadForm(request.POST)
		if form.is_valid():
			mac = form.data['mactofind']
			entries = form.data['entries']
			days = form.data['days']
			return HttpResponseRedirect('handledownloadupload', {'mac' : mac, 'entries' : entries,})
		
	
	else:	
		form = DownloadUploadForm
		return render (request, 'bydownloadupload.html', {'form' : form})
			
	
	
def handledownloadupload (request):


	'''If the user got here by entering it in the address bar, redirect to previous page'''
	if request.method == "GET":
		form = DownloadUploadForm
		return HttpResponseRedirect('bydownloadupload', {'form' : form})
		
	else:
	
		'''Grab data from previous page'''
		mac = request.POST.get('mactofind')
		entries = request.POST.get('entries')
		days = request.POST.get('days')
		
		'''If the amount of entries was left blank, the user has indicated there should be no limit. Thus, set the entries to a very high number'''
		if entries == "":
			entries = 999999
			
		else:
			entries = int(entries)
			
		'''Find out how long to look back based on how many days the user entered'''
		days = int(days)
		daysago = timezone.now() - timedelta(days=days)
		date = timezone.now()
		
		
		'''Grab reports according to critera provided'''
		reports = Report.objects.values('date', 'download', 'upload').filter(date__range=(daysago, date), mac=mac).order_by('-date')[0:entries]
		
		
		'''If the list of reports is blank, try to see if the mac entered was a nickname instead and search by that. If still blank, just return the empty query.'''
		if not reports.exists():
			try:
				newmac = Macname.objects.values_list('mac', flat = True).filter(nickname=mac)[0]
				reports = Report.objects.values('date', 'download', 'upload').filter(date__range=(daysago, date), mac=newmac).order_by('-date')[0:entries]
				
			except:
				reports = ()
			
		
		
		return render (request, 'handledownloadupload.html', {'reports' : reports, 'mac' : mac, 'entries' : entries})
		
		
	
@method_decorator(csrf_exempt, name='dispatch')
def nickbulk (request):
	'''If posting the form, save all the form data to a variable and pass it on'''
	if request.method == "POST":
		form = NickBulkForm(request.POST)
		if form.is_valid():
			data = form.data
			return HttpResponseRedirect('handlenickbulk', {'data' : data})
			
	
	else:
		form = NickBulkForm
		return render (request, 'nickbulk.html', {'form' : form})
		
		
@method_decorator(csrf_exempt, name='dispatch')
def handlenickbulk (request):
	
	data = request.POST
	
	'''Loop through the key-value pairs in the data. Will match up each MAC with their nickname and save it, whether it's changed or not.'''
	try:
		for key, value in data.items():
			macname = Macname.objects.get(id=key)
			macname.nickname = value
			macname.save()
	except:
		pass
	
	'''Then return to the viewing page.'''
	macnames = Macname.objects.all()
	return render (request, 'macnamesview.html', {'macnames' : macnames})
	
	
	
def bydeviceusage (request):

	if request.method == "POST":
		form = DeviceUsageForm(request.POST)
		if form.is_valid():
			mactofind = form.data['mactofind']
			device = form.data['device']
			
			return HttpResponseRedirect('handledeviceusage', {'mac' : mac, 'device' : device})
			
	
	
	else:
		form = DeviceUsageForm
		return render(request, 'bydeviceusage.html', {'form' : form})
	
	

def handledeviceusage (request):
	
	'''If the user got here by entering it in the address bar and not by submitting a form, return to previous page'''
	if request.method == "GET":
		form = DeviceUsageForm
		return HttpResponseRedirect('bydeviceusage', {'form' : form})
	
	
	mac = request.POST.get('mactofind')
	device = request.POST.get('device')
	
	'''Get data for the given MAC address'''
	newest = Report.objects.values_list('date', flat = True).order_by('date').filter(mac=mac, device=device)
	
	'''If that doesn't work, try to see if there's a nickname matching what was entered and search by that instead.'''
	if not newest.exists():
		newmac = Macname.objects.values_list('mac', flat = True).filter(nickname=mac)[0]
		newest = Report.objects.values_list('date', flat = True).order_by('date').filter(mac=newmac, device=device)
		
		
	'''A session is 30 minutes long. It is determined fairly simply - it takes the first use, then adds another session once it finds a use that is 30 minutes past the first one.
	More or less the best way to go about it for the time being.'''
	sessions = 1
	
	for item in newest:
	
		try:
			if item > last:
				sessions = sessions + 1
			last = item + timedelta(minutes=30)
		except:
			pass
			
		last = item + timedelta(minutes=30)
		
		
	'''The time spent is to be shown as hours, so just divide by 2'''
	hours = (sessions / 2)
	
	
	return render(request, 'handledeviceusage.html', {'mac' : mac, 'device' : device, 'hours' : hours, 'sessions' : sessions, 'newest' : newest, 'last' : last})
	

	
	


	

	
