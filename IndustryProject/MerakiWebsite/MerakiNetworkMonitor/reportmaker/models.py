from __future__ import unicode_literals
from django.db import models

# Create your models here.

class Report(models.Model):
	date = models.DateTimeField()
	mac = models.TextField()
	site = models.TextField()
	source = models.TextField()
	destination = models.TextField()
	device = models.TextField()
	download = models.TextField()
	upload = models.TextField()
	
	
	def __str__(self):
		return self.mac + " - " + str(self.date)
		
		



class Macname(models.Model):
	id = models.AutoField(primary_key = True)
	mac = models.TextField(unique = True)
	nickname = models.TextField()
	
	def __str__(self):
		return self.nickname + " " + self.mac + " " + str(self.id)
		

def getMAC(line):
    wordtofind = 'mac='
    
    currentmac = (line[line.index(wordtofind) + len(wordtofind):])
    currentmac = (currentmac[0:18])
    currentmac = currentmac.strip()
    currentmac = currentmac.replace("'", "")

  

    return currentmac



def getDate(line):
	currentdate = (line[0:24])
	month = ""
	
	if "Jan" in currentdate:
		month = "01"
		
	if "Feb" in currentdate:
		month = "02"
		
	if "Mar" in currentdate:
		month = "03"
		
	if "Apr" in currentdate:
		month = "04"
		
	if "May" in currentdate:
		month = "05"
		
	if "Jun" in currentdate:
		month = "06"
		
	if "Jul" in currentdate:
		month = "07"
		
	if "Aug" in currentdate:
		month = "08"
		
	if "Sep" in currentdate:
		month = "09"
		
	if "Oct" in currentdate:
		month = "10"
		
	if "Nov" in currentdate:
		month = "11"
		
	if "Dec" in currentdate:
		month = "12"
		
	day = (currentdate[7:10])
	
	time = (currentdate[10:19])
	
	year = (currentdate[19:24])
	
	day = day.strip()
	time = time.strip()
	year = year.strip()
	
	
	return year + "-" + month + "-" + day + " " + time
	'''year-month-day time'''
		
		
		
	
		
	
		
		
		
		
		
	return (currentdate)	
	

# Grabs the site accessed. Expect a lot of 'junk' data at present..
# If exception, return none
def getSite(line):
    wordtofind = 'request:'
    try:
        currentsite = (line[line.index(wordtofind) + len(wordtofind):])
        currentsite = (currentsite[5:])
        currentsite = currentsite.replace("...\n", "")
        currentsite = currentsite.replace("OWN","")



    except:
        return "None"

    return currentsite

	
# Grab the Request type (GET, POST, usually UNKNOWN) from the line
# If exception, return none
def getRequestType(line):
    wordtofind = 'request:'
    try:
        currentrequest = (line[line.index(wordtofind) + len(wordtofind):])
        currentrequest = (currentrequest[0:8])

    except:
        return "No Request Type Associated"
    return currentrequest

	
# Get the source IP from each line
# If exception, return none
def getSourceIP(line):
    wordtofind = 'src='
    try:
        currentip = (line[line.index(wordtofind) + len(wordtofind):])
        currentip = (currentip[0:20])

        sep = ' '
        currentip = currentip.split(sep, 1)[0]

        if currentip == None:
            currentip = "No Source IP Associated"

    except:
        return "None"
    return currentip

	
# Get the destination IP
# If exception, returns such a statement
def getDestinationIP(line):
    wordtofind = 'dst='
    try:
        currentip = (line[line.index(wordtofind) + len(wordtofind):])
        currentip = (currentip[0:18])

        sep = ' '
        currentip = currentip.split(sep, 1)[0]

        if currentip == None:
            currentip = "No Destination IP Associated"

    except:
        return "None"
    return currentip

	
#GRabs the amount of data downloaded (in bits per second)
def getDownload(line):
    wordtofind = 'download='
    try:
        currentdownload = (line[line.index(wordtofind) + len(wordtofind) :])
        currentdownload = (currentdownload[0:15])
        currentdownload = currentdownload.replace('"', "")

        if currentdownload == None:
            currentdownload = "None"

    except:
        return "No Download"

    return currentdownload


	
# Grabs the amount of data uploaded (in bits per second)
def getUpload(line):
    wordtofind = 'upload='
    try:
        currentupload = (line[line.index(wordtofind) + len(wordtofind) :])

        if currentupload == None:
            currentupload = "None"

    except:
        return "No Upload"

    return currentupload
	


def getDevice(line):
	device = None
	if "Fred_appliance" in line:
		device = "Fred's Appliance"
		
	if "ACE_MR42" in line:
		device = "ACE_MR42"
		
	if device == None:
		device = "None"
		
	return device

	

	


	

	

