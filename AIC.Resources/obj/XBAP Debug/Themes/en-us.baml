   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   	strServer  strSecondaryserver  strLogin  strLock  	strUnLock  strUser  strUserLogin  strRole  strPassword 		 strOK 

 strYes 	 strNo  	strCancel  strClose  strUserError  strPermissionError  strExit  strExitSystem  	strLogout  strLogoutSystem  strStart  strStop  strItem  	strDevice  strOrganization  strMainControlCard  strWireMatchingCard  strIEPESlot " strEddyCurrentDisplacementSlot  strEddyCurrentKeyPhaseSlot   strEddyCurrentTachometerSlot  strDigitTachometerSlot   strAnalogRransducerInSlot ! strRelaySlot " strDigitRransducerInSlot # strDigitRransducerOutSlot $ strAnalogRransducerOutSlot % strWaveGeneralSlot & strSlot ' 
strChannel ( 	strDanger ) strAlarm * strPreAlarm + 	strNormal , strAbnormal - strUnConnect . tabFirst / menuUser 0 	menuLogin 1 
menuLogout 2 menuLock 3 
menuUnLock 4 menuExit 5 menuEditPwd 6 	menuTheme 7 	menuStyle 8 
menuWindow 9 menuShowInTaskbar : menuTopmost ; menuMaximizeFullScreen < menuHelp = 	menuAbout > menuFeedback ? menuAbnormalList @ menuSystemLog A 
menuManage B menuUserManage C menuRoleManage D menuMenuManage E menuOrganizationManage F menuManageLog G menuDeviceData H menuDeviceRunTime I menuDeviceRunAnalyze J menuDeviceHourlyData K menuDeviceFaultDiagnose L menuEditDeviceComponents M menuEditDeviceTemplate N menuDBManage O menuExportDBData P menuImportDBData Q menuFilterDBData R menuQucikData S menuServerQucikData T menuDeviceQucikData U menuItemQucikData V menuDiagnose W menuBPNetWorks X menuSOMNetWorks Y strStartTrain Z strTestTrain [ strLoadNetWorks \ strSaveNetWorks ] strUnloadNetWorks ^ menuGeneralManage _ menuSystemManage ` menuLocalSetting a menuHistoryPlaySetting b menuAboutVersion c menuScreenShotFile d menuSetting e menuServerSetting f menuCollectorSetting g menuEquipmentSetting h menuData i menuOnlineData j menuHistoricalData k menuAlartmData l menuRunningMonitor m menuRunningAnalyze n menuLanguage o menuChinese p menuEnglish q menuOnlineDataList r menuOnlineDataTile s menuOnlineDataDiagram t menuOnlineDataOverview u menuOnlineDataDiagnosis v menuOnlineDataStatistics w menuDataTrendChart x menuHistoryDataList y menuHistoryDataDiagram z menuHistoryDataStatistics { menuSystemEventList | messagePDAManage1 } messagePDAManage2 ~ messagePDAManage3  messagePDAManage4 � messagePDAManage5 � messagePDAManage6 � messagePDAManage7 � messagePDAManage8 � messagePDAManage9 � messagePDAManage10 � messagePDAManage11 � messagePDAManage12 � messagePDAManage13 
� delete 
� unbind 
� search 	� query � add � edit � default � 
serverName � serverIP � 
serverPort � serverManagement � organizationName � 	longitude � latitude � defaultlogin � 
servermode � loginResult � permissions � primaryserverIP 
� upload � refresh 	� pdaip � senddown � transmissionIdentifier � save � load 	� print 
� export 
� import 	� built � set � name � code � 	aliasName � password � role � menu 
� button � organization � personTelephone � officeTelephone 	� email � contact 
� sortNo � isAdmin � isSuperAdmin � type � internalNumber � showText � 	isChecked � checkedExpanded � operateRecord � operateType � operateDateTime � remarks � end � 
deviceName � groupByDevice � addattachment 	� pause � play � 
screenShot 	� alarm � mute 	� clear � more � rememberUser � rememberPwd � templateManage � addData�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     ,s&clr-namespace:System;assembly=mscorlib  %�  &	        &	      &	 %     &	 4     &	 B     &	 R     &	 `     &	 t     &	 �     &		 �     &	
 �     &	 �     &	 �     &	 �     &	 �     &	 �     &	     &	 &    &	 <    &	 L    &	 b    &	 q    &	     &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    &	     &	 >    &	 a    &	  ~    &	! �    &	" �    &	# �    &	$ �    &	%     &	& *    &	' 8    &	( I    &	) Y    &	* h    &	+ z    &	, �    &	- �    &	. �    &	/ �    &	0 �    &	1 �    &	2 �    &	3 �    &	4     &	5     &	6 ,    &	7 ;    &	8 J    &	9 Z    &	: q    &	; �    &	< �    &	= �    &	> �    &	? �    &	@ �    &	A �    &	B     &	C     &	D .    &	E B    &	F ^    &	G r    &	H �    &	I �    &	J �    &	K �    &	L �    &	M     &	N .    &	O @    &	P V    &	Q l    &	R �    &	S �    &	T �    &	U �    &	V �    &	W �    &	X     &	Y     &	Z -    &	[ @    &	\ V    &	] l    &	^ �    &	_ �    &	` �    &	a �    &	b �    &	c �    &	d     &	e      &	f 7    &	g Q    &	h k    &	i y    &	j �    &	k �    &	l �    &	m �    &	n �    &	o �    &	p 	    &	q 	    &	r 5	    &	s M	    &	t h	    &	u �	    &	v �	    &	w �	    &	x �	    &	y �	    &	z 	
    &	{ ,
    &	| F
    &	} �
    &	~ �
    &	 �
    &	�     &	� i    &	� �    &	�     &	� 5    &	� j    &	� �    &	�     &	� H    &	� �    &	� �    &	� �    &	� �    &	� �    &	� �    &	� �    &	�     &	�     &	� '    &	� ;    &	� U    &	� o    &	� �    &	� �    &	� �    &	� �    &	� �    &	� �    &	�     &	�     &	� "    &	� 1    &	� C    &	� g    &	� u    &	� �    &	� �    &	� �    &	� �    &	� �    &	� �    &	� �    &	� �    &	� �    &	�     &	�     &	� +    &	� ;    &	� Q    &	� j    &	� �    &	� �    &	� �    &	� �    &	� �    &	� �    &	� �    &	�      &	�     &	� %    &	� >    &	� U    &	� n    &	� �    &	� �    &	� �    &	� �    &	� �    &	� �    &	� �    &	�     &	�     &	� '    &	� 5    &	� D    &	� R    &	� h    &	� �    &	� �    ��
Server����Multiserver����	Login����Lock����
UnLock����User����
User Login����Role����Password����OK����Yes����No����
Cancel����	Close����User name or password wrong����Permission denied����Exit����Exit system?����
Logout����Logout User?����	Start����Stop����Item����
Device����Organization����MainControlCard����WireMatchingCard����IEPESlot����EddyCurrentDisplacementSlot����EddyCurrentKeyPhaseSlot����EddyCurrentTachometerSlot����DigitTachometerSlot����AnalogRransducerInSlot����	RelaySlot����DigitRransducerInSlot����DigitRransducerOutSlot����AnalogRransducerOutSlot����WaveGeneralSlot����Slot����Channel����
Danger����	Alarm����PreAlarm����
Normal����Abnormal����	UnConnect����Home����User����	Login����
Logout����Lock����
UnLock����Exit����EditPassword����	Theme����	Style����
Window����ShowInTaskbar����Topmost����MaximizeFullScreen����Help����	About����Feedback����AbnormalList����	SystemLog����
Manage����
UserManage����
RoleManage����
MenuManage����OrganizationManage����
SettingLog����
DeviceData����DeviceRunStatus����DeviceRunAnalyze����DeviceHourlyData����DeviceFaultDiagnose����EditDeviceComponents����menuEditDeviceTemplate����DBManage����ExportDBData����ImportDBData����SearchDBData����	QucikData����ServerQucikData����DeviceQucikData����ItemQucikData����Diagnose����
BPNetWorks����SOMNetWorks����
StartTrain����	TestTrain����LoadNetWorks����SaveNetWorks����UnloadNetWorks����GeneralManage����SystemManage����LocalSetting����HistoryPlaySettin����AboutVersion����ScreenShotFile����
System����ServerSetting����CollectorSetting����EquipmentSetting����Data����
OnlineData����HistoricalData����
AlartmData����RunningMonitor����RunningAnalyze����Language����Chinese����English����OnlineDataList����OnlineDataTile����OnlineDataDiagram����OnlineDataOverview����OnlineDataDiagnosis����OnlineAlarmStatistics����DataTrendChart����HistoryDataList����HistoryDiagram����HistoryDataDataStatistics����HistoryEventList����95This type cannot be added to a sub-frequency！！！����3/Please bind the measuring points first！！！����51Determine to delete the organization structure？����Determine unbinding？����JFThe test point has been bound, please reselect the test point！！！����73Please select the test point to be unbound！！！����YUThe number receiver already exists in the server without having to repeat it！！！����*&Select the main board to copy！！！����/+Empty recycle bin is not supported！！！����A=The test point has been bound and cannot be restored！！！����MIThe corresponding channel does not exist and cannot be recovered！！！����>:The channel has been bound and cannot be restored！！！����HDPlease select the test points in the organization structure！！！����
Delete����
Unbind����
Search����	Query����Add����Edit����Default����
ServerName����ServerIP����
ServerPort����ServerManagement����OrganizationName����	Longitude����Latitude����Defaultlogin����
Servermode����LoginResult����Permissions����PrimaryServerIP����
Upload����Refresh����	PDAIP����SendDown����TransmissionCardIdentifier����Save����Load����	Print����
Export����
Import����	Built����Set����Name����Code����	AliasName����Password����Role����Menu����
Button����Organization����PersonTelephone����OfficeTelephone����	Email����Contact����
SortNo����IsAdmin����IsSuperAdmin����Type����InternalNumber����ShowText����	IsChecked����CheckedExpanded����OperateRecord����UserOperateType����OperateDateTime����Remarks����End����
DeviceName����GroupByDevice����AddAttachment����	Pause����Play����
ScreenShot����	Alarm����Mute����	Clear����More����RememberUser����RememberPassword����TemplateManage����AddData��