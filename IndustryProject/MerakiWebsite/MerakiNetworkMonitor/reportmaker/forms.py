from django import forms
from .models import *



class ReportForm(forms.ModelForm):
	class Meta:
		model = Report
		fields = ('date', 'mac', 'site', 'source', 'destination', 'device', 'download', 'upload',)
		

class ReportUploadForm(forms.Form):
		
		file = forms.FileField(label = "Select File To Upload:")
		
	
class RawForm (forms.Form):
	raw = forms.CharField(widget = forms.Textarea(), label = "")
	
	
class MACInputForm (forms.Form):
	mactofind = forms.CharField(label = "Find all entries for this MAC address or nickname:")
	entries = forms.IntegerField(label = "Retrieve only this number of the most recent entries (leave blank for no limit):", required = False, initial = 50)
	days = forms.IntegerField(label = "In this past number of days:", initial = 7)
	
	
class NickInputForm(forms.ModelForm):
	class Meta:
		model = Macname
		fields = ('nickname', 'mac')
		
	
class AccessInputForm(forms.Form):
	access = forms.CharField(label = "Access Point To Find:")
	entries = forms.IntegerField(label = "Retrieve only this number of the most recent entries (leave blank for no limit):", required = False, initial = 50)
	days = forms.IntegerField(label = "In this past number of days:", initial = 7)

	
class SiteInputForm(forms.Form):
	mactofind = forms.CharField(label = "Get websites for this address or nickname:")
	entries = forms.IntegerField(label = "Retrieve the following amount of latest entries (leave blank for no limit):", required = False, initial = 50)
	days = forms.IntegerField(label = "In this past number of days:", initial = 7)
	

class DownloadUploadForm(forms.Form):
	mactofind = forms.CharField(label = "Get uploads and downloads for this address or nickname:")
	entries = forms.IntegerField(label = "Retrieve the following amount of latest entries (leave blank for no limit):", required = False, initial = 50)
	days = forms.IntegerField(label = "In this past number of days:", initial = 7)
	
		
class FindNickForm(forms.Form):
	nick = forms.CharField(label = "Enter nickname to look for:")
	entries = forms.IntegerField(label = "Retrieve the following amount of latest entries (leave blank for no limit):", required = False, initial = 50)
	days = forms.IntegerField(label = "In this past number of days:", initial = 7)
	
	
class NickBulkForm(forms.Form):
	
	nicknames = Macname.objects.all()
	
	def __init__(self, *args, **kwargs):
		super(NickBulkForm, self).__init__(*args, **kwargs)
		nicknames = Macname.objects.all()
		for nickname in nicknames:
			self.fields[nickname.id] = forms.CharField(label = nickname.mac, initial = nickname.nickname)
		
	
class DeviceUsageForm(forms.Form):

	mactofind = forms.CharField(label = "Enter MAC or nickname to look for:")
	device = forms.CharField(label = "Enter name of device to look for:")
	
	
	

	