from django.urls import path
from . import views

urlpatterns = [
    path('', views.homepage, name='homepage'),
	path('viewreports', views.viewreports, name = 'viewreports'),
	path('bymac', views.bymac, name = 'bymac'),
	path('choosereportinput', views.choosereportinput, name = 'choosereportinput'),
	path('addreports', views.addreports, name = 'addreports'),
	path('addreportsfile', views.addreportsfile, name = 'addreportsfile'),
	path('deletereports', views.deletereports, name = 'deletereports'),
	path('handlereports', views.handlereports, name = 'handlereports'),
	path('handlereportsfile', views.handlereportsfile, name = 'handlereportsfile'),
	path('handledeletereports', views.handledeletereports, name = 'handledeletereports'),
	path('handlemac', views.handlemac, name = 'handlemac'),
	path('macnamesview', views.macnamesview, name = 'macnamesview'),
	path ('byaccess', views.byaccess, name = "byaccess"),
	path('handleaccess', views.handleaccess, name = "handleaccess"),
	path('bysite', views.bysite, name = "bysite"),
	path('handlesite', views.handlesite, name = "handlesite"),
	path('bydownloadupload', views.bydownloadupload, name = "bydownloadupload"),
	path('handledownloadupload', views.handledownloadupload, name = "handledownloadupload"),
	path('nickbulk', views.nickbulk, name = 'nickbulk'),
	path('handlenickbulk', views.handlenickbulk, name = 'handlenickbulk'),
	path('bydeviceusage', views.bydeviceusage, name = 'bydeviceusage'),
	path('handledeviceusage', views.handledeviceusage, name = 'handledeviceusage'),
]