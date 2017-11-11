using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DatabaseService.Tests
{
    static class JsonTestAddWrieless
    {
        public static string jsonstr = @"{
    'MainControlCard': {
        'ACQ_Unit_Type': 0,
        'AliasName': 'AIC SYSTEM Card',
        'AsySyn': 0,
        'CommunicationCategory': [
            {
                'Code': 0,
                'Name': 'RJ45'
            },
            {
                'Code': 1,
                'Name': 'WIFI'
            },
            {
                'Code': 2,
                'Name': '4G'
            },
            {
                'Code': 3,
                'Name': '3G'
            }
        ],
        'CommunicationCode': 0,
        'ControllerIP': '',
        'DataSourceCategory': [
            {
                'Code': 0,
                'Name': 'Hd ACQ'
            },
            {
                'Code': 1,
                'Name': 'SD'
            }
        ],
        'DataSourceCode': 0,
        'Identifier': '',
        'IsAlarmLatch': false,
        'IsConfiguration': true,
        'IsHdBypass': false,
        'IsHdConfiguration': false,
        'IsHdMultiplication': false,
        'IsListen': true,
        'LanguageCategory': [
            {
                'Code': 0,
                'Name': '汉语'
            },
            {
                'Code': 1,
                'Name': 'english'
            }
        ],
        'LanguageCode': 0,
        'MainCardCategory': [
            {
                'Code': 0,
                'Name': '在线主板'
            },
            {
                'Code': 1,
                'Name': '保护主板'
            }
        ],
        'MainCardCode': 0,
        'SampleMode': {
            'Code': 0,
            'EqualAngleSample': {
                'CHNum': -1,
                'CardNum': -1,
                'Code': 3,
                'Name': '等角度',
                'SlotNum': -1
            },
            'EqualCycleSample': {
                'CHNum': -1,
                'CardNum': -1,
                'Code': 2,
                'Name': '等周期',
                'ReferenceCycleCount': 20,
                'SlotNum': -1
            },
            'FreeSample': {
                'Code': 0,
                'Name': '自由',
                'SampleFre': 2560,
                'SamplePoint': 2048
            },
            'RPMTriggerSample': {
                'CHNum': -1,
                'CardNum': -1,
                'Code': 1,
                'Name': '转速触发',
                'SampleFre': 2560,
                'SamplePoint': 2048,
                'SlotNum': -1
            }
        },
        'ScaleDataRange': 16384,
        'ServerIP': '',
        'SynWaveCode': 0,
        'Version': '01.01.01.170510.02',
        'WaveCategory': [
            {
                'Code': 0,
                'Name': '瞬态'
            },
            {
                'Code': 1,
                'Name': '连续'
            }
        ]
    },
    'WireMatchingCard': [
        {
            'AnalogRransducerInSlot': {
                'AnalogRransducerInChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
                'CardNum': 0,
                'InSignalCategory': [
                    {
                        'Code': 0,
                        'Name': '有源4-20MA'
                    },
                    {
                        'Code': 1,
                        'Name': '无源4-20MA'
                    }
                ],
                'InSignalCode': 0,
                'IsInput': true,
                'SlotName': '模拟变送器输入',
                'SlotNum': 5,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'AnalogRransducerOutSlot': {
                'AnalogRransducerOutChannelInfo': [
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
                'CardNum': 0,
                'InSignalCategory': [
                ],
                'InSignalCode': 33,
                'IsInput': false,
                'SlotName': '模拟变送器输出',
                'SlotNum': 9,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'CardName': 'card0',
            'CardNum': 0,
            'DigitRransducerInSlot': {
                'CardNum': 0,
                'DigitRransducerInChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'EnableModBus485': true,
                        'EnableModBusTCPIP': false,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'ModBus485': {
                        },
                        'ModBusFunCategory': [
                            {
                                'Code': 2,
                                'Name': '2号功能码'
                            },
                            {
                                'Code': 3,
                                'Name': '3号功能码'
                            },
                            {
                                'Code': 4,
                                'Name': '4号功能码'
                            }
                        ],
                        'ModBusFunCode': 4,
                        'ModBusTCPIP': {
                        },
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'SwitchCategory': [
                            {
                                'Code': 0,
                                'IsFisrtItemofTRUE': true,
                                'Name': '开/关'
                            },
                            {
                                'Code': 1,
                                'IsFisrtItemofTRUE': true,
                                'Name': '断开/合上'
                            },
                            {
                                'Code': 2,
                                'IsFisrtItemofTRUE': true,
                                'Name': '亮/灭'
                            },
                            {
                                'Code': 3,
                                'IsFisrtItemofTRUE': true,
                                'Name': '红色/绿色'
                            }
                        ],
                        'SwitchCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                ],
                'InSignalCode': 33,
                'IsInput': true,
                'SlotName': '数字变送器输入',
                'SlotNum': 7,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'DigitRransducerOutSlot': {
                'CardNum': 0,
                'DigitRransducerOutChannelInfo': [
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'EnableModBus485': true,
                        'EnableModBusTCPIP': false,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'ModBus485': {
                        },
                        'ModBusFunCategory': [
                            {
                                'Code': 2,
                                'Name': '2号功能码'
                            },
                            {
                                'Code': 3,
                                'Name': '3号功能码'
                            },
                            {
                                'Code': 4,
                                'Name': '4号功能码'
                            }
                        ],
                        'ModBusFunCode': 4,
                        'ModBusTCPIP': {
                        },
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'SwitchCategory': [
                            {
                                'Code': 0,
                                'IsFisrtItemofTRUE': true,
                                'Name': '开/关'
                            },
                            {
                                'Code': 1,
                                'IsFisrtItemofTRUE': true,
                                'Name': '断开/合上'
                            },
                            {
                                'Code': 2,
                                'IsFisrtItemofTRUE': true,
                                'Name': '亮/灭'
                            },
                            {
                                'Code': 3,
                                'IsFisrtItemofTRUE': true,
                                'Name': '红色/绿色'
                            }
                        ],
                        'SwitchCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                ],
                'InSignalCode': 33,
                'IsInput': false,
                'SlotName': '数字变送器输出',
                'SlotNum': 8,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'DigitTachometerSlot': {
                'CardNum': 0,
                'DigitTachometerChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'CHNum': 0,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'TeethNumber': 1,
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'CHNum': 1,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'TeethNumber': 1,
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'CHNum': 2,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'TeethNumber': 1,
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'CHNum': 3,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'TeethNumber': 1,
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                    {
                        'Code': 0,
                        'Name': '24V 电平'
                    },
                    {
                        'Code': 1,
                        'Name': '5V 电平'
                    },
                    {
                        'Code': 2,
                        'Name': '磁电式传感器'
                    },
                    {
                        'Code': 3,
                        'Name': '磁阻式传感器'
                    }
                ],
                'InSignalCode': 0,
                'IsInput': true,
                'SlotName': '数字转速表',
                'SlotNum': 4,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'EddyCurrentDisplacementSlot': {
                'CardNum': 0,
                'EddyCurrentDisplacementChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 39,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 4,
                                'Name': '轴振动',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 5,
                                'Name': '轴位移',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 6,
                                'Name': '胀差',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 4,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 0,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 4,
                                'Name': '轴振动',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 5,
                                'Name': '轴位移',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 6,
                                'Name': '胀差',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 4,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 720279,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 4,
                                'Name': '轴振动',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 5,
                                'Name': '轴位移',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 6,
                                'Name': '胀差',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 4,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 0,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 4,
                                'Name': '轴振动',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 5,
                                'Name': '轴位移',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 6,
                                'Name': '胀差',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 4,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': '',
                        'WaveUnit': ''
                    }
                ],
                'HighPassCategory': [
                    {
                        'Code': 0,
                        'Name': '0.1HZ'
                    },
                    {
                        'Code': 1,
                        'Name': '400HZ'
                    },
                    {
                        'Code': 2,
                        'Name': '1000HZ'
                    }
                ],
                'HighPassCode': 0,
                'InSignalCode': 0,
                'Is24V': false,
                'SampleFre': 0,
                'SampleMode': {
                    'Code': 0,
                    'EqualAngleSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 3,
                        'Name': '等角度',
                        'SlotNum': -1
                    },
                    'EqualCycleSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 2,
                        'Name': '等周期',
                        'ReferenceCycleCount': 20,
                        'SlotNum': -1
                    },
                    'FreeSample': {
                        'Code': 0,
                        'Name': '自由',
                        'SampleFre': 2560,
                        'SamplePoint': 2048
                    },
                    'RPMTriggerSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 1,
                        'Name': '转速触发',
                        'SampleFre': 2560,
                        'SamplePoint': 2048,
                        'SlotNum': -1
                    }
                },
                'SamplePoint': 0,
                'SlotNum': 1,
                'Unit': 'um',
                'WaveCategory': [
                    {
                        'Code': 0,
                        'Name': '瞬态'
                    },
                    {
                        'Code': 1,
                        'Name': '连续'
                    }
                ],
                'WaveCode': 0
            },
            'EddyCurrentKeyPhaseSlot': {
                'CardNum': 0,
                'EddyCurrentKeyPhaseChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 0,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'HysteresisVolt': 0.25,
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'IsValidWave': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'TeethNumber': 1,
                        'ThresholdModeCategory': [
                            {
                                'Code': 0,
                                'Name': 'Manual'
                            },
                            {
                                'Code': 1,
                                'Name': 'Auto'
                            }
                        ],
                        'ThresholdModeCode': 0,
                        'ThresholdVolt': -12,
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 1,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'HysteresisVolt': 0.25,
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'IsValidWave': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'TeethNumber': 1,
                        'ThresholdModeCategory': [
                            {
                                'Code': 0,
                                'Name': 'Manual'
                            },
                            {
                                'Code': 1,
                                'Name': 'Auto'
                            }
                        ],
                        'ThresholdModeCode': 0,
                        'ThresholdVolt': -12,
                        'Unit': '',
                        'WaveUnit': ''
                    }
                ],
                'EddyCurrentRPMCode': 2,
                'EddyCurrentRPMSample': [
                    {
                        'Code': 0,
                        'Name': '自动',
                        'SampleFre': -1
                    },
                    {
                        'Code': 1,
                        'Name': '低频',
                        'SampleFre': 512
                    },
                    {
                        'Code': 2,
                        'Name': '中频',
                        'SampleFre': 3200
                    },
                    {
                        'Code': 3,
                        'Name': '高频',
                        'SampleFre': 25600
                    }
                ],
                'InSignalCode': 0,
                'Is24V': false,
                'SlotNum': 2,
                'Unit': 'RPM'
            },
            'EddyCurrentTachometerSlot': {
                'CardNum': 0,
                'EddyCurrentRPMCode': 2,
                'EddyCurrentRPMSample': [
                    {
                        'Code': 0,
                        'Name': '自动',
                        'SampleFre': -1
                    },
                    {
                        'Code': 1,
                        'Name': '低频',
                        'SampleFre': 512
                    },
                    {
                        'Code': 2,
                        'Name': '中频',
                        'SampleFre': 3200
                    },
                    {
                        'Code': 3,
                        'Name': '高频',
                        'SampleFre': 25600
                    }
                ],
                'EddyCurrentTachometerChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 0,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'HysteresisVolt': 0.25,
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'IsValidWave': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'RPMCouplingCategory': [
                            {
                                'Code': 0,
                                'Name': '反转'
                            },
                            {
                                'Code': 1,
                                'Name': '零转速'
                            },
                            {
                                'Code': 2,
                                'Name': '转速'
                            }
                        ],
                        'RPMCouplingCode': 0,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'TeethNumber': 1,
                        'ThresholdModeCategory': [
                            {
                                'Code': 0,
                                'Name': 'Manual'
                            },
                            {
                                'Code': 1,
                                'Name': 'Auto'
                            }
                        ],
                        'ThresholdModeCode': 0,
                        'ThresholdVolt': -12,
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 1,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'HysteresisVolt': 0.25,
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'IsValidWave': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'RPMCouplingCategory': [
                            {
                                'Code': 0,
                                'Name': '反转'
                            },
                            {
                                'Code': 1,
                                'Name': '零转速'
                            },
                            {
                                'Code': 2,
                                'Name': '转速'
                            }
                        ],
                        'RPMCouplingCode': 0,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'TeethNumber': 1,
                        'ThresholdModeCategory': [
                            {
                                'Code': 0,
                                'Name': 'Manual'
                            },
                            {
                                'Code': 1,
                                'Name': 'Auto'
                            }
                        ],
                        'ThresholdModeCode': 0,
                        'ThresholdVolt': -12,
                        'Unit': '',
                        'WaveUnit': ''
                    }
                ],
                'InSignalCode': 0,
                'Is24V': false,
                'IsEnableMainCH': true,
                'SlotNum': 3,
                'Unit': 'RPM'
            },
            'IEPESlot': {
                'CardNum': 0,
                'HighPassCategory': [
                    {
                        'Code': 0,
                        'Name': '0.1HZ'
                    },
                    {
                        'Code': 1,
                        'Name': '400HZ'
                    },
                    {
                        'Code': 2,
                        'Name': '1000HZ'
                    }
                ],
                'HighPassCode': 0,
                'IEPEChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': 21,
                        'BiasVoltLow': 3,
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'DisplacementCalibration': 1,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 39,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 0,
                                'Name': '有效值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 1,
                                'Name': '峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 2,
                                'Name': '峰峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 0,
                        'Sensitivity': 0.096000000834465027,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': '',
                        'VelocityCalibration': 1,
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': 21,
                        'BiasVoltLow': 3,
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'DisplacementCalibration': 1,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 413431836,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 0,
                                'Name': '有效值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 1,
                                'Name': '峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 2,
                                'Name': '峰峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 0,
                        'Sensitivity': 0.096000000834465027,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': '',
                        'VelocityCalibration': 1,
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': 21,
                        'BiasVoltLow': 3,
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'DisplacementCalibration': 1,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 65,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 0,
                                'Name': '有效值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 1,
                                'Name': '峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 2,
                                'Name': '峰峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 0,
                        'Sensitivity': 0.096000000834465027,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': '',
                        'VelocityCalibration': 1,
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': 21,
                        'BiasVoltLow': 3,
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'DisplacementCalibration': 1,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 0,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 0,
                                'Name': '有效值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 1,
                                'Name': '峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 2,
                                'Name': '峰峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 0,
                        'Sensitivity': 0.096000000834465027,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': '',
                        'VelocityCalibration': 1,
                        'WaveUnit': ''
                    }
                ],
                'InSignalCode': 0,
                'Integration': 0,
                'SampleFre': 0,
                'SampleMode': {
                    'Code': 0,
                    'EqualAngleSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 3,
                        'Name': '等角度',
                        'SlotNum': -1
                    },
                    'EqualCycleSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 2,
                        'Name': '等周期',
                        'ReferenceCycleCount': 20,
                        'SlotNum': -1
                    },
                    'FreeSample': {
                        'Code': 0,
                        'Name': '自由',
                        'SampleFre': 2560,
                        'SamplePoint': 2048
                    },
                    'RPMTriggerSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 1,
                        'Name': '转速触发',
                        'SampleFre': 2560,
                        'SamplePoint': 2048,
                        'SlotNum': -1
                    }
                },
                'SamplePoint': 0,
                'SlotNum': 0,
                'Unit': 'm/s^2',
                'WaveCategory': [
                    {
                        'Code': 0,
                        'Name': '瞬态'
                    },
                    {
                        'Code': 1,
                        'Name': '连续'
                    }
                ],
                'WaveCode': 0
            },
            'RelaySlot': {
                'CardNum': 0,
                'InSignalCategory': [
                ],
                'InSignalCode': 33,
                'IsInput': false,
                'RelayChannelInfo': [
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 6619253,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 7667820,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
                'SlotName': '继电器',
                'SlotNum': 6,
                'UploadIntevalTime': 10000,
                'Version': ''
            }
        },
        {
            'AnalogRransducerInSlot': {
                'AnalogRransducerInChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
                'CardNum': 1,
                'InSignalCategory': [
                    {
                        'Code': 0,
                        'Name': '有源4-20MA'
                    },
                    {
                        'Code': 1,
                        'Name': '无源4-20MA'
                    }
                ],
                'InSignalCode': 0,
                'IsInput': true,
                'SlotName': '模拟变送器输入',
                'SlotNum': 5,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'AnalogRransducerOutSlot': {
                'AnalogRransducerOutChannelInfo': [
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
                'CardNum': 1,
                'InSignalCategory': [
                ],
                'InSignalCode': -2147483615,
                'IsInput': false,
                'SlotName': '模拟变送器输出',
                'SlotNum': 9,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'CardName': 'card1',
            'CardNum': 1,
            'DigitRransducerInSlot': {
                'CardNum': 1,
                'DigitRransducerInChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'EnableModBus485': true,
                        'EnableModBusTCPIP': false,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'ModBus485': {
                        },
                        'ModBusFunCategory': [
                            {
                                'Code': 2,
                                'Name': '2号功能码'
                            },
                            {
                                'Code': 3,
                                'Name': '3号功能码'
                            },
                            {
                                'Code': 4,
                                'Name': '4号功能码'
                            }
                        ],
                        'ModBusFunCode': 4,
                        'ModBusTCPIP': {
                        },
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'SwitchCategory': [
                            {
                                'Code': 0,
                                'IsFisrtItemofTRUE': true,
                                'Name': '开/关'
                            },
                            {
                                'Code': 1,
                                'IsFisrtItemofTRUE': true,
                                'Name': '断开/合上'
                            },
                            {
                                'Code': 2,
                                'IsFisrtItemofTRUE': true,
                                'Name': '亮/灭'
                            },
                            {
                                'Code': 3,
                                'IsFisrtItemofTRUE': true,
                                'Name': '红色/绿色'
                            }
                        ],
                        'SwitchCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                ],
                'InSignalCode': -2147483615,
                'IsInput': true,
                'SlotName': '数字变送器输入',
                'SlotNum': 7,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'DigitRransducerOutSlot': {
                'CardNum': 1,
                'DigitRransducerOutChannelInfo': [
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'EnableModBus485': true,
                        'EnableModBusTCPIP': false,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'ModBus485': {
                        },
                        'ModBusFunCategory': [
                            {
                                'Code': 2,
                                'Name': '2号功能码'
                            },
                            {
                                'Code': 3,
                                'Name': '3号功能码'
                            },
                            {
                                'Code': 4,
                                'Name': '4号功能码'
                            }
                        ],
                        'ModBusFunCode': 4,
                        'ModBusTCPIP': {
                        },
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'SwitchCategory': [
                            {
                                'Code': 0,
                                'IsFisrtItemofTRUE': true,
                                'Name': '开/关'
                            },
                            {
                                'Code': 1,
                                'IsFisrtItemofTRUE': true,
                                'Name': '断开/合上'
                            },
                            {
                                'Code': 2,
                                'IsFisrtItemofTRUE': true,
                                'Name': '亮/灭'
                            },
                            {
                                'Code': 3,
                                'IsFisrtItemofTRUE': true,
                                'Name': '红色/绿色'
                            }
                        ],
                        'SwitchCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                ],
                'InSignalCode': 1,
                'IsInput': false,
                'SlotName': '数字变送器输出',
                'SlotNum': 8,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'DigitTachometerSlot': {
                'CardNum': 1,
                'DigitTachometerChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'CHNum': 0,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'TeethNumber': 1,
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'CHNum': 1,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'TeethNumber': 1,
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'CHNum': 2,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'TeethNumber': 1,
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'CHNum': 3,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'TeethNumber': 1,
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                    {
                        'Code': 0,
                        'Name': '24V 电平'
                    },
                    {
                        'Code': 1,
                        'Name': '5V 电平'
                    },
                    {
                        'Code': 2,
                        'Name': '磁电式传感器'
                    },
                    {
                        'Code': 3,
                        'Name': '磁阻式传感器'
                    }
                ],
                'InSignalCode': 0,
                'IsInput': true,
                'SlotName': '数字转速表',
                'SlotNum': 4,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'EddyCurrentDisplacementSlot': {
                'CardNum': 1,
                'EddyCurrentDisplacementChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 39,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 4,
                                'Name': '轴振动',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 5,
                                'Name': '轴位移',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 6,
                                'Name': '胀差',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 4,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 0,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 4,
                                'Name': '轴振动',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 5,
                                'Name': '轴位移',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 6,
                                'Name': '胀差',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 4,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 851351,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 4,
                                'Name': '轴振动',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 5,
                                'Name': '轴位移',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 6,
                                'Name': '胀差',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 4,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 0,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 4,
                                'Name': '轴振动',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 5,
                                'Name': '轴位移',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 6,
                                'Name': '胀差',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 4,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': '',
                        'WaveUnit': ''
                    }
                ],
                'HighPassCategory': [
                    {
                        'Code': 0,
                        'Name': '0.1HZ'
                    },
                    {
                        'Code': 1,
                        'Name': '400HZ'
                    },
                    {
                        'Code': 2,
                        'Name': '1000HZ'
                    }
                ],
                'HighPassCode': 0,
                'InSignalCode': 0,
                'Is24V': false,
                'SampleFre': 0,
                'SampleMode': {
                    'Code': 0,
                    'EqualAngleSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 3,
                        'Name': '等角度',
                        'SlotNum': -1
                    },
                    'EqualCycleSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 2,
                        'Name': '等周期',
                        'ReferenceCycleCount': 20,
                        'SlotNum': -1
                    },
                    'FreeSample': {
                        'Code': 0,
                        'Name': '自由',
                        'SampleFre': 2560,
                        'SamplePoint': 2048
                    },
                    'RPMTriggerSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 1,
                        'Name': '转速触发',
                        'SampleFre': 2560,
                        'SamplePoint': 2048,
                        'SlotNum': -1
                    }
                },
                'SamplePoint': 0,
                'SlotNum': 1,
                'Unit': 'um',
                'WaveCategory': [
                    {
                        'Code': 0,
                        'Name': '瞬态'
                    },
                    {
                        'Code': 1,
                        'Name': '连续'
                    }
                ],
                'WaveCode': 0
            },
            'EddyCurrentKeyPhaseSlot': {
                'CardNum': 1,
                'EddyCurrentKeyPhaseChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 0,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'HysteresisVolt': 0.25,
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'IsValidWave': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'TeethNumber': 1,
                        'ThresholdModeCategory': [
                            {
                                'Code': 0,
                                'Name': 'Manual'
                            },
                            {
                                'Code': 1,
                                'Name': 'Auto'
                            }
                        ],
                        'ThresholdModeCode': 0,
                        'ThresholdVolt': -12,
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 1,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'HysteresisVolt': 0.25,
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'IsValidWave': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'TeethNumber': 1,
                        'ThresholdModeCategory': [
                            {
                                'Code': 0,
                                'Name': 'Manual'
                            },
                            {
                                'Code': 1,
                                'Name': 'Auto'
                            }
                        ],
                        'ThresholdModeCode': 0,
                        'ThresholdVolt': -12,
                        'Unit': '',
                        'WaveUnit': ''
                    }
                ],
                'EddyCurrentRPMCode': 2,
                'EddyCurrentRPMSample': [
                    {
                        'Code': 0,
                        'Name': '自动',
                        'SampleFre': -1
                    },
                    {
                        'Code': 1,
                        'Name': '低频',
                        'SampleFre': 512
                    },
                    {
                        'Code': 2,
                        'Name': '中频',
                        'SampleFre': 3200
                    },
                    {
                        'Code': 3,
                        'Name': '高频',
                        'SampleFre': 25600
                    }
                ],
                'InSignalCode': 0,
                'Is24V': false,
                'SlotNum': 2,
                'Unit': 'RPM'
            },
            'EddyCurrentTachometerSlot': {
                'CardNum': 1,
                'EddyCurrentRPMCode': 2,
                'EddyCurrentRPMSample': [
                    {
                        'Code': 0,
                        'Name': '自动',
                        'SampleFre': -1
                    },
                    {
                        'Code': 1,
                        'Name': '低频',
                        'SampleFre': 512
                    },
                    {
                        'Code': 2,
                        'Name': '中频',
                        'SampleFre': 3200
                    },
                    {
                        'Code': 3,
                        'Name': '高频',
                        'SampleFre': 25600
                    }
                ],
                'EddyCurrentTachometerChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 0,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'HysteresisVolt': 0.25,
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'IsValidWave': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'RPMCouplingCategory': [
                            {
                                'Code': 0,
                                'Name': '反转'
                            },
                            {
                                'Code': 1,
                                'Name': '零转速'
                            },
                            {
                                'Code': 2,
                                'Name': '转速'
                            }
                        ],
                        'RPMCouplingCode': 0,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'TeethNumber': 1,
                        'ThresholdModeCategory': [
                            {
                                'Code': 0,
                                'Name': 'Manual'
                            },
                            {
                                'Code': 1,
                                'Name': 'Auto'
                            }
                        ],
                        'ThresholdModeCode': 0,
                        'ThresholdVolt': -12,
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 1,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'HysteresisVolt': 0.25,
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'IsValidWave': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'RPMCouplingCategory': [
                            {
                                'Code': 0,
                                'Name': '反转'
                            },
                            {
                                'Code': 1,
                                'Name': '零转速'
                            },
                            {
                                'Code': 2,
                                'Name': '转速'
                            }
                        ],
                        'RPMCouplingCode': 0,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'TeethNumber': 1,
                        'ThresholdModeCategory': [
                            {
                                'Code': 0,
                                'Name': 'Manual'
                            },
                            {
                                'Code': 1,
                                'Name': 'Auto'
                            }
                        ],
                        'ThresholdModeCode': 0,
                        'ThresholdVolt': -12,
                        'Unit': '',
                        'WaveUnit': ''
                    }
                ],
                'InSignalCode': 0,
                'Is24V': false,
                'IsEnableMainCH': true,
                'SlotNum': 3,
                'Unit': 'RPM'
            },
            'IEPESlot': {
                'CardNum': 1,
                'HighPassCategory': [
                    {
                        'Code': 0,
                        'Name': '0.1HZ'
                    },
                    {
                        'Code': 1,
                        'Name': '400HZ'
                    },
                    {
                        'Code': 2,
                        'Name': '1000HZ'
                    }
                ],
                'HighPassCode': 0,
                'IEPEChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': 21,
                        'BiasVoltLow': 3,
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'DisplacementCalibration': 1,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 39,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 0,
                                'Name': '有效值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 1,
                                'Name': '峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 2,
                                'Name': '峰峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 0,
                        'Sensitivity': 0.096000000834465027,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': '',
                        'VelocityCalibration': 1,
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': 21,
                        'BiasVoltLow': 3,
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'DisplacementCalibration': 1,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 413565844,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 0,
                                'Name': '有效值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 1,
                                'Name': '峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 2,
                                'Name': '峰峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 0,
                        'Sensitivity': 0.096000000834465027,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': '',
                        'VelocityCalibration': 1,
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': 21,
                        'BiasVoltLow': 3,
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'DisplacementCalibration': 1,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 0,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 0,
                                'Name': '有效值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 1,
                                'Name': '峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 2,
                                'Name': '峰峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 0,
                        'Sensitivity': 0.096000000834465027,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': '',
                        'VelocityCalibration': 1,
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': 21,
                        'BiasVoltLow': 3,
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'DisplacementCalibration': 1,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 0,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 0,
                                'Name': '有效值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 1,
                                'Name': '峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 2,
                                'Name': '峰峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 0,
                        'Sensitivity': 0.096000000834465027,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': '',
                        'VelocityCalibration': 1,
                        'WaveUnit': ''
                    }
                ],
                'InSignalCode': 0,
                'Integration': 0,
                'SampleFre': 0,
                'SampleMode': {
                    'Code': 0,
                    'EqualAngleSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 3,
                        'Name': '等角度',
                        'SlotNum': -1
                    },
                    'EqualCycleSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 2,
                        'Name': '等周期',
                        'ReferenceCycleCount': 20,
                        'SlotNum': -1
                    },
                    'FreeSample': {
                        'Code': 0,
                        'Name': '自由',
                        'SampleFre': 2560,
                        'SamplePoint': 2048
                    },
                    'RPMTriggerSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 1,
                        'Name': '转速触发',
                        'SampleFre': 2560,
                        'SamplePoint': 2048,
                        'SlotNum': -1
                    }
                },
                'SamplePoint': 0,
                'SlotNum': 0,
                'Unit': 'm/s^2',
                'WaveCategory': [
                    {
                        'Code': 0,
                        'Name': '瞬态'
                    },
                    {
                        'Code': 1,
                        'Name': '连续'
                    }
                ],
                'WaveCode': 0
            },
            'RelaySlot': {
                'CardNum': 1,
                'InSignalCategory': [
                ],
                'InSignalCode': 0,
                'IsInput': false,
                'RelayChannelInfo': [
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
                'SlotName': '继电器',
                'SlotNum': 6,
                'UploadIntevalTime': 10000,
                'Version': ''
            }
        },
        {
            'AnalogRransducerInSlot': {
                'AnalogRransducerInChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
                'CardNum': 2,
                'InSignalCategory': [
                    {
                        'Code': 0,
                        'Name': '有源4-20MA'
                    },
                    {
                        'Code': 1,
                        'Name': '无源4-20MA'
                    }
                ],
                'InSignalCode': 0,
                'IsInput': true,
                'SlotName': '模拟变送器输入',
                'SlotNum': 5,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'AnalogRransducerOutSlot': {
                'AnalogRransducerOutChannelInfo': [
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
                'CardNum': 2,
                'InSignalCategory': [
                ],
                'InSignalCode': 0,
                'IsInput': false,
                'SlotName': '模拟变送器输出',
                'SlotNum': 9,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'CardName': 'card2',
            'CardNum': 2,
            'DigitRransducerInSlot': {
                'CardNum': 2,
                'DigitRransducerInChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'EnableModBus485': true,
                        'EnableModBusTCPIP': false,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'ModBus485': {
                        },
                        'ModBusFunCategory': [
                            {
                                'Code': 2,
                                'Name': '2号功能码'
                            },
                            {
                                'Code': 3,
                                'Name': '3号功能码'
                            },
                            {
                                'Code': 4,
                                'Name': '4号功能码'
                            }
                        ],
                        'ModBusFunCode': 4,
                        'ModBusTCPIP': {
                        },
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'SwitchCategory': [
                            {
                                'Code': 0,
                                'IsFisrtItemofTRUE': true,
                                'Name': '开/关'
                            },
                            {
                                'Code': 1,
                                'IsFisrtItemofTRUE': true,
                                'Name': '断开/合上'
                            },
                            {
                                'Code': 2,
                                'IsFisrtItemofTRUE': true,
                                'Name': '亮/灭'
                            },
                            {
                                'Code': 3,
                                'IsFisrtItemofTRUE': true,
                                'Name': '红色/绿色'
                            }
                        ],
                        'SwitchCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                ],
                'InSignalCode': 0,
                'IsInput': true,
                'SlotName': '数字变送器输入',
                'SlotNum': 7,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'DigitRransducerOutSlot': {
                'CardNum': 2,
                'DigitRransducerOutChannelInfo': [
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'EnableModBus485': true,
                        'EnableModBusTCPIP': false,
                        'EquationCategory': [
                            {
                                'CalibrationCor': 1,
                                'Code': 0,
                                'Formula': '',
                                'Name': '一次线性方程'
                            },
                            {
                                'CalibrationCor': 1,
                                'Code': 1,
                                'Formula': '',
                                'Name': '一次非线性方程'
                            }
                        ],
                        'EquationCode': 0,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'ModBus485': {
                        },
                        'ModBusFunCategory': [
                            {
                                'Code': 2,
                                'Name': '2号功能码'
                            },
                            {
                                'Code': 3,
                                'Name': '3号功能码'
                            },
                            {
                                'Code': 4,
                                'Name': '4号功能码'
                            }
                        ],
                        'ModBusFunCode': 4,
                        'ModBusTCPIP': {
                        },
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SourceCHNum': -1,
                        'SourceCardNum': -1,
                        'SourceSlotNum': -1,
                        'SourceSubCHNum': -1,
                        'SubCHNum': 0,
                        'SwitchCategory': [
                            {
                                'Code': 0,
                                'IsFisrtItemofTRUE': true,
                                'Name': '开/关'
                            },
                            {
                                'Code': 1,
                                'IsFisrtItemofTRUE': true,
                                'Name': '断开/合上'
                            },
                            {
                                'Code': 2,
                                'IsFisrtItemofTRUE': true,
                                'Name': '亮/灭'
                            },
                            {
                                'Code': 3,
                                'IsFisrtItemofTRUE': true,
                                'Name': '红色/绿色'
                            }
                        ],
                        'SwitchCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                ],
                'InSignalCode': 0,
                'IsInput': false,
                'SlotName': '数字变送器输出',
                'SlotNum': 8,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'DigitTachometerSlot': {
                'CardNum': 2,
                'DigitTachometerChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'CHNum': 0,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'TeethNumber': 1,
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'CHNum': 1,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'TeethNumber': 1,
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'CHNum': 2,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'TeethNumber': 1,
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'CHNum': 3,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'TeethNumber': 1,
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                    {
                        'Code': 0,
                        'Name': '24V 电平'
                    },
                    {
                        'Code': 1,
                        'Name': '5V 电平'
                    },
                    {
                        'Code': 2,
                        'Name': '磁电式传感器'
                    },
                    {
                        'Code': 3,
                        'Name': '磁阻式传感器'
                    }
                ],
                'InSignalCode': 0,
                'IsInput': true,
                'SlotName': '数字转速表',
                'SlotNum': 4,
                'UploadIntevalTime': 10000,
                'Version': ''
            },
            'EddyCurrentDisplacementSlot': {
                'CardNum': 2,
                'EddyCurrentDisplacementChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 0,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 4,
                                'Name': '轴振动',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 5,
                                'Name': '轴位移',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 6,
                                'Name': '胀差',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 4,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 0,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 4,
                                'Name': '轴振动',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 5,
                                'Name': '轴位移',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 6,
                                'Name': '胀差',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 4,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 0,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 4,
                                'Name': '轴振动',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 5,
                                'Name': '轴位移',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 6,
                                'Name': '胀差',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 4,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 0,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 4,
                                'Name': '轴振动',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 5,
                                'Name': '轴位移',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 6,
                                'Name': '胀差',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 4,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': '',
                        'WaveUnit': ''
                    }
                ],
                'HighPassCategory': [
                    {
                        'Code': 0,
                        'Name': '0.1HZ'
                    },
                    {
                        'Code': 1,
                        'Name': '400HZ'
                    },
                    {
                        'Code': 2,
                        'Name': '1000HZ'
                    }
                ],
                'HighPassCode': 0,
                'InSignalCode': 0,
                'Is24V': false,
                'SampleFre': 0,
                'SampleMode': {
                    'Code': 0,
                    'EqualAngleSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 3,
                        'Name': '等角度',
                        'SlotNum': -1
                    },
                    'EqualCycleSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 2,
                        'Name': '等周期',
                        'ReferenceCycleCount': 20,
                        'SlotNum': -1
                    },
                    'FreeSample': {
                        'Code': 0,
                        'Name': '自由',
                        'SampleFre': 2560,
                        'SamplePoint': 2048
                    },
                    'RPMTriggerSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 1,
                        'Name': '转速触发',
                        'SampleFre': 2560,
                        'SamplePoint': 2048,
                        'SlotNum': -1
                    }
                },
                'SamplePoint': 0,
                'SlotNum': 1,
                'Unit': 'um',
                'WaveCategory': [
                    {
                        'Code': 0,
                        'Name': '瞬态'
                    },
                    {
                        'Code': 1,
                        'Name': '连续'
                    }
                ],
                'WaveCode': 0
            },
            'EddyCurrentKeyPhaseSlot': {
                'CardNum': 2,
                'EddyCurrentKeyPhaseChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 0,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'HysteresisVolt': 0.25,
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'IsValidWave': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'TeethNumber': 1,
                        'ThresholdModeCategory': [
                            {
                                'Code': 0,
                                'Name': 'Manual'
                            },
                            {
                                'Code': 1,
                                'Name': 'Auto'
                            }
                        ],
                        'ThresholdModeCode': 0,
                        'ThresholdVolt': -12,
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 1,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'HysteresisVolt': 0.25,
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'IsValidWave': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'TeethNumber': 1,
                        'ThresholdModeCategory': [
                            {
                                'Code': 0,
                                'Name': 'Manual'
                            },
                            {
                                'Code': 1,
                                'Name': 'Auto'
                            }
                        ],
                        'ThresholdModeCode': 0,
                        'ThresholdVolt': -12,
                        'Unit': '',
                        'WaveUnit': ''
                    }
                ],
                'EddyCurrentRPMCode': 2,
                'EddyCurrentRPMSample': [
                    {
                        'Code': 0,
                        'Name': '自动',
                        'SampleFre': -1
                    },
                    {
                        'Code': 1,
                        'Name': '低频',
                        'SampleFre': 512
                    },
                    {
                        'Code': 2,
                        'Name': '中频',
                        'SampleFre': 3200
                    },
                    {
                        'Code': 3,
                        'Name': '高频',
                        'SampleFre': 25600
                    }
                ],
                'InSignalCode': 0,
                'Is24V': false,
                'SlotNum': 2,
                'Unit': 'RPM'
            },
            'EddyCurrentTachometerSlot': {
                'CardNum': 2,
                'EddyCurrentRPMCode': 2,
                'EddyCurrentRPMSample': [
                    {
                        'Code': 0,
                        'Name': '自动',
                        'SampleFre': -1
                    },
                    {
                        'Code': 1,
                        'Name': '低频',
                        'SampleFre': 512
                    },
                    {
                        'Code': 2,
                        'Name': '中频',
                        'SampleFre': 3200
                    },
                    {
                        'Code': 3,
                        'Name': '高频',
                        'SampleFre': 25600
                    }
                ],
                'EddyCurrentTachometerChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 0,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'HysteresisVolt': 0.25,
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'IsValidWave': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'RPMCouplingCategory': [
                            {
                                'Code': 0,
                                'Name': '反转'
                            },
                            {
                                'Code': 1,
                                'Name': '零转速'
                            },
                            {
                                'Code': 2,
                                'Name': '转速'
                            }
                        ],
                        'RPMCouplingCode': 0,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'TeethNumber': 1,
                        'ThresholdModeCategory': [
                            {
                                'Code': 0,
                                'Name': 'Manual'
                            },
                            {
                                'Code': 1,
                                'Name': 'Auto'
                            }
                        ],
                        'ThresholdModeCode': 0,
                        'ThresholdVolt': -12,
                        'Unit': '',
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 4,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '正常',
                                        'Value': 0
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'AverageNumber': 1,
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 1,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'HysteresisVolt': 0.25,
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsNotch': true,
                        'IsUploadData': false,
                        'IsValidWave': false,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'RPMCouplingCategory': [
                            {
                                'Code': 0,
                                'Name': '反转'
                            },
                            {
                                'Code': 1,
                                'Name': '零转速'
                            },
                            {
                                'Code': 2,
                                'Name': '转速'
                            }
                        ],
                        'RPMCouplingCode': 0,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0.079999998211860657,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'TeethNumber': 1,
                        'ThresholdModeCategory': [
                            {
                                'Code': 0,
                                'Name': 'Manual'
                            },
                            {
                                'Code': 1,
                                'Name': 'Auto'
                            }
                        ],
                        'ThresholdModeCode': 0,
                        'ThresholdVolt': -12,
                        'Unit': '',
                        'WaveUnit': ''
                    }
                ],
                'InSignalCode': 0,
                'Is24V': false,
                'IsEnableMainCH': true,
                'SlotNum': 3,
                'Unit': 'RPM'
            },
            'IEPESlot': {
                'CardNum': 2,
                'HighPassCategory': [
                    {
                        'Code': 0,
                        'Name': '0.1HZ'
                    },
                    {
                        'Code': 1,
                        'Name': '400HZ'
                    },
                    {
                        'Code': 2,
                        'Name': '1000HZ'
                    }
                ],
                'HighPassCode': 0,
                'IEPEChannelInfo': [
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': 21,
                        'BiasVoltLow': 3,
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'DisplacementCalibration': 1,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 0,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 0,
                                'Name': '有效值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 1,
                                'Name': '峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 2,
                                'Name': '峰峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 0,
                        'Sensitivity': 0.096000000834465027,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': '',
                        'VelocityCalibration': 1,
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': 21,
                        'BiasVoltLow': 3,
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'DisplacementCalibration': 1,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 43,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 0,
                                'Name': '有效值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 1,
                                'Name': '峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 2,
                                'Name': '峰峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 0,
                        'Sensitivity': 0.096000000834465027,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': '',
                        'VelocityCalibration': 1,
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': 21,
                        'BiasVoltLow': 3,
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'DisplacementCalibration': 1,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 0,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 0,
                                'Name': '有效值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 1,
                                'Name': '峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 2,
                                'Name': '峰峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 0,
                        'Sensitivity': 0.096000000834465027,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': '',
                        'VelocityCalibration': 1,
                        'WaveUnit': ''
                    },
                    {
                        'AlarmStrategy': {
                            'Absolute': {
                                'Category': [
                                    {
                                        'Code': 0,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高危险',
                                        'Value': 15
                                    },
                                    {
                                        'Code': 1,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高警告',
                                        'Value': 10
                                    },
                                    {
                                        'Code': 2,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高预警告',
                                        'Value': 6
                                    },
                                    {
                                        'Code': 3,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '高正常',
                                        'Value': 3
                                    },
                                    {
                                        'Code': 5,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低正常',
                                        'Value': -3.5
                                    },
                                    {
                                        'Code': 6,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低预警告',
                                        'Value': -6.4000000953674316
                                    },
                                    {
                                        'Code': 7,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低警告',
                                        'Value': -10
                                    },
                                    {
                                        'Code': 8,
                                        'Formula': '',
                                        'IsAllow': true,
                                        'Name': '低危险',
                                        'Value': -10
                                    }
                                ],
                                'Mode': [
                                    {
                                        'Code': 0,
                                        'Name': '自动'
                                    },
                                    {
                                        'Code': 1,
                                        'Name': '单值'
                                    },
                                    {
                                        'Code': 2,
                                        'Name': '曲线或曲面'
                                    }
                                ],
                                'ModeCode': 0,
                                'Para': [
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    },
                                    {
                                        'CHNum': 1,
                                        'CardNum': 2
                                    }
                                ]
                            },
                            'Comparative': {
                                'IntevalTime': 10000,
                                'IsAllow': false,
                                'Para': [
                                    {
                                        'CHNum': 0,
                                        'CardNum': 1
                                    }
                                ],
                                'Percent': 0.20000000298023224,
                                'Range': ''
                            }
                        },
                        'BiasVoltHigh': 21,
                        'BiasVoltLow': 3,
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'DisplacementCalibration': 1,
                        'DivFreInfo': [
                            {
                                'AlarmStrategy': {
                                    'Absolute': {
                                        'Category': [
                                            {
                                                'Code': 0,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高危险',
                                                'Value': 15
                                            },
                                            {
                                                'Code': 1,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高警告',
                                                'Value': 10
                                            },
                                            {
                                                'Code': 2,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '高预警告',
                                                'Value': 6
                                            },
                                            {
                                                'Code': 4,
                                                'Formula': '',
                                                'IsAllow': true,
                                                'Name': '正常',
                                                'Value': 0
                                            }
                                        ]
                                    },
                                    'Comparative': {
                                        'IntevalTime': 10000,
                                        'IsAllow': false,
                                        'Para': [
                                            {
                                                'CHNum': 0,
                                                'CardNum': 1
                                            }
                                        ],
                                        'Percent': 0.20000000298023224,
                                        'Range': ''
                                    }
                                },
                                'BasedOnRPM': {
                                    'Code': 0,
                                    'MultiFre': 1,
                                    'Name': '基于转速'
                                },
                                'BasedOnRange': {
                                    'Code': 2,
                                    'FreHigh': 4.157122826702563e+26,
                                    'FreLow': 4.157122826702563e+26,
                                    'MaxFreNum': 4.157122826702563e+26,
                                    'Name': '基于范围'
                                },
                                'Code': '',
                                'Create_Time': '',
                                'DivFreCode': -1,
                                'FixedFre': {
                                    'CharacteristicFre': 80,
                                    'Code': 1,
                                    'Name': '固定频率',
                                    'Percent': 0.05000000074505806
                                },
                                'Guid': '',
                                'Modify_Time': '',
                                'Name': 'example',
                                'Remarks': '',
                                'T_Item_Code': '',
                                'T_Item_Guid': '',
                                'T_Item_Name': ''
                            }
                        ],
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsMultiplication': false,
                        'IsSaveWaveToSD': true,
                        'IsUploadData': false,
                        'IsUploadWave': true,
                        'IsValidWave': true,
                        'LocalSaveCategory': [
                            {
                                'Code': 0,
                                'Name': '上传失败且报警数据'
                            },
                            {
                                'Code': 1,
                                'Name': '本地备份报警数据'
                            },
                            {
                                'Code': 2,
                                'Name': '上传失败就存储数据'
                            },
                            {
                                'Code': 3,
                                'Name': '本地备份所有数据'
                            },
                            {
                                'Code': 4,
                                'Name': '不存任何数据'
                            }
                        ],
                        'LocalSaveCode': 4,
                        'LogicExpression': '',
                        'MultiplicationCor': 1,
                        'Organization': [
                        ],
                        'PRMSlotNum': 0,
                        'RPMCHNum': 0,
                        'RPMCardNum': 1,
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 0,
                                'Name': '有效值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 1,
                                'Name': '峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 2,
                                'Name': '峰峰值',
                                'ZeroValue': 0
                            },
                            {
                                'Code': 3,
                                'Name': '平均值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 0,
                        'Sensitivity': 0.096000000834465027,
                        'SubCHNum': 0,
                        'TPDirCategory': [
                            {
                                'Code': 0,
                                'Degree': 90,
                                'Name': 'Radial direction'
                            },
                            {
                                'Code': 1,
                                'Degree': 0,
                                'Name': 'Axial direction'
                            }
                        ],
                        'TPDirCode': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': '',
                        'VelocityCalibration': 1,
                        'WaveUnit': ''
                    }
                ],
                'InSignalCode': 0,
                'Integration': 0,
                'SampleFre': 0,
                'SampleMode': {
                    'Code': 0,
                    'EqualAngleSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 3,
                        'Name': '等角度',
                        'SlotNum': -1
                    },
                    'EqualCycleSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 2,
                        'Name': '等周期',
                        'ReferenceCycleCount': 20,
                        'SlotNum': -1
                    },
                    'FreeSample': {
                        'Code': 0,
                        'Name': '自由',
                        'SampleFre': 2560,
                        'SamplePoint': 2048
                    },
                    'RPMTriggerSample': {
                        'CHNum': -1,
                        'CardNum': -1,
                        'Code': 1,
                        'Name': '转速触发',
                        'SampleFre': 2560,
                        'SamplePoint': 2048,
                        'SlotNum': -1
                    }
                },
                'SamplePoint': 0,
                'SlotNum': 0,
                'Unit': 'm/s^2',
                'WaveCategory': [
                    {
                        'Code': 0,
                        'Name': '瞬态'
                    },
                    {
                        'Code': 1,
                        'Name': '连续'
                    }
                ],
                'WaveCode': 0
            },
            'RelaySlot': {
                'CardNum': 2,
                'InSignalCategory': [
                ],
                'InSignalCode': 0,
                'IsInput': false,
                'RelayChannelInfo': [
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 1,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号1',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 2,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号2',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': ''
                    },
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 3,
                        'DelayAlarmTime': 10000,
                        'ExtraInfo': {
                        },
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': [
                        ],
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'SubCHNum': 0,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
                'SlotName': '继电器',
                'SlotNum': 6,
                'UploadIntevalTime': 10000,
                'Version': ''
            }
        }
    ],
    'WirelessReceiveCard': {
        'MasterIdentifier': 'FA010C05AF',
        'ReceiveCardName': '',
        'TransmissionCard': [
            {
                'BatteryEnergy': 0.80000001192092896,
                'ExtraInfo': {
                },
                'Remarks': '',
                'SlaveIdentifier': 'FE010C05A1',
                'SleepTime': 0,
                'TransmissionName': '',
                'TransmissionType': 0,
                'Version': '',
                'WirelessScalarSlot': {
                    'SlotNum': 0,
                    'WirelessScalarChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 4,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '正常',
                                            'Value': 0
                                        }
                                    ]
                                }
                            },
                            'CHNum': 4,
                            'DelayAlarmTime': 10000,
                            'EquationCategory': [
                                {
                                    'CalibrationCor': 1,
                                    'Code': 0,
                                    'Formula': '',
                                    'Name': '一次线性方程'
                                },
                                {
                                    'CalibrationCor': 1,
                                    'Code': 1,
                                    'Formula': '',
                                    'Name': '一次非线性方程'
                                }
                            ],
                            'EquationCode': 0,
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsUploadData': false,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'Organization': [
                            ],
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 7,
                                    'Name': '数值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 7,
                            'SubCHNum': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号4',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点4',
                            'Unit': ''
                        },
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 4,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '正常',
                                            'Value': 0
                                        }
                                    ]
                                }
                            },
                            'CHNum': 5,
                            'DelayAlarmTime': 10000,
                            'EquationCategory': [
                                {
                                    'CalibrationCor': 1,
                                    'Code': 0,
                                    'Formula': '',
                                    'Name': '一次线性方程'
                                },
                                {
                                    'CalibrationCor': 1,
                                    'Code': 1,
                                    'Formula': '',
                                    'Name': '一次非线性方程'
                                }
                            ],
                            'EquationCode': 0,
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsUploadData': false,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'Organization': [
                            ],
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 7,
                                    'Name': '数值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 7,
                            'SubCHNum': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号5',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点5',
                            'Unit': ''
                        },
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 4,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '正常',
                                            'Value': 0
                                        }
                                    ]
                                }
                            },
                            'CHNum': 6,
                            'DelayAlarmTime': 10000,
                            'EquationCategory': [
                                {
                                    'CalibrationCor': 1,
                                    'Code': 0,
                                    'Formula': '',
                                    'Name': '一次线性方程'
                                },
                                {
                                    'CalibrationCor': 1,
                                    'Code': 1,
                                    'Formula': '',
                                    'Name': '一次非线性方程'
                                }
                            ],
                            'EquationCode': 0,
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsUploadData': false,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'Organization': [
                            ],
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 7,
                                    'Name': '数值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 7,
                            'SubCHNum': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号6',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点6',
                            'Unit': ''
                        }
                    ]
                },
                'WirelessVibrationSlot': {
                    'Integration': 1,
                    'SampleFreCategory': [
                        {
                            'Code': 7,
                            'Name': '338HZ'
                        },
                        {
                            'Code': 6,
                            'Name': '675HZ'
                        },
                        {
                            'Code': 5,
                            'Name': '1.35KHZ'
                        },
                        {
                            'Code': 4,
                            'Name': '2.7KHZ'
                        },
                        {
                            'Code': 3,
                            'Name': '5.4KHZ'
                        },
                        {
                            'Code': 2,
                            'Name': '10.8KHZ'
                        },
                        {
                            'Code': 1,
                            'Name': '21.6KHZ'
                        },
                        {
                            'Code': 0,
                            'Name': '43.2KHZ'
                        }
                    ],
                    'SampleFreCode': 4,
                    'SamplePointCategory': [
                        {
                            'Code': 0,
                            'Name': '256'
                        },
                        {
                            'Code': 1,
                            'Name': '512'
                        },
                        {
                            'Code': 2,
                            'Name': '1024'
                        },
                        {
                            'Code': 3,
                            'Name': '2048'
                        },
                        {
                            'Code': 4,
                            'Name': '4096'
                        },
                        {
                            'Code': 5,
                            'Name': '8192'
                        },
                        {
                            'Code': 6,
                            'Name': '16384'
                        }
                    ],
                    'SamplePointCode': 3,
                    'SlotNum': 1,
                    'Unit': 'mm/s',
                    'WirelessVibrationChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 3,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高正常',
                                            'Value': 3
                                        },
                                        {
                                            'Code': 5,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低正常',
                                            'Value': -3.5
                                        },
                                        {
                                            'Code': 6,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低预警告',
                                            'Value': -6.4000000953674316
                                        },
                                        {
                                            'Code': 7,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低警告',
                                            'Value': -10
                                        },
                                        {
                                            'Code': 8,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低危险',
                                            'Value': -10
                                        }
                                    ],
                                    'Mode': [
                                        {
                                            'Code': 0,
                                            'Name': '自动'
                                        },
                                        {
                                            'Code': 1,
                                            'Name': '单值'
                                        },
                                        {
                                            'Code': 2,
                                            'Name': '曲线或曲面'
                                        }
                                    ],
                                    'ModeCode': 0,
                                    'Para': [
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        },
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        }
                                    ]
                                }
                            },
                            'BiasVoltHigh': 5,
                            'BiasVoltLow': 0,
                            'CHNum': 7,
                            'DelayAlarmTime': 10000,
                            'DisplacementCalibration': 1,
                            'DivFreInfo': [
                                {
                                    'AlarmStrategy': {
                                        'Absolute': {
                                            'Category': [
                                                {
                                                    'Code': 0,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高危险',
                                                    'Value': 15
                                                },
                                                {
                                                    'Code': 1,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高警告',
                                                    'Value': 10
                                                },
                                                {
                                                    'Code': 2,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高预警告',
                                                    'Value': 6
                                                },
                                                {
                                                    'Code': 4,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '正常',
                                                    'Value': 0
                                                }
                                            ]
                                        },
                                        'Comparative': {
                                            'IntevalTime': 10000,
                                            'IsAllow': false,
                                            'Para': [
                                                {
                                                    'CHNum': 0,
                                                    'CardNum': 1
                                                }
                                            ],
                                            'Percent': 0.20000000298023224,
                                            'Range': ''
                                        }
                                    },
                                    'BasedOnRPM': {
                                        'Code': 0,
                                        'MultiFre': 1,
                                        'Name': '基于转速'
                                    },
                                    'BasedOnRange': {
                                        'Code': 2,
                                        'FreHigh': 0,
                                        'FreLow': 0,
                                        'MaxFreNum': 0,
                                        'Name': '基于范围'
                                    },
                                    'Code': '',
                                    'Create_Time': '',
                                    'DivFreCode': -1,
                                    'FixedFre': {
                                        'CharacteristicFre': 80,
                                        'Code': 1,
                                        'Name': '固定频率',
                                        'Percent': 0.05000000074505806
                                    },
                                    'Guid': '',
                                    'Modify_Time': '',
                                    'Name': 'example',
                                    'Remarks': '',
                                    'T_Item_Code': '',
                                    'T_Item_Guid': '',
                                    'T_Item_Name': ''
                                }
                            ],
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsMultiplication': false,
                            'IsSaveWaveToSD': true,
                            'IsUploadData': false,
                            'IsUploadWave': true,
                            'IsValidWave': true,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'MultiplicationCor': 1,
                            'Organization': [
                            ],
                            'PRMSlotNum': 0,
                            'RPMCHNum': 0,
                            'RPMCardNum': 1,
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 0,
                                    'Name': '有效值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 1,
                                    'Name': '峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 2,
                                    'Name': '峰峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 3,
                                    'Name': '平均值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 0,
                            'Sensitivity': 0.096000000834465027,
                            'SubCHNum': 0,
                            'TPDirCategory': [
                                {
                                    'Code': 0,
                                    'Degree': 90,
                                    'Name': 'Radial direction'
                                },
                                {
                                    'Code': 1,
                                    'Degree': 0,
                                    'Name': 'Axial direction'
                                }
                            ],
                            'TPDirCode': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号7',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点7',
                            'Unit': '',
                            'VelocityCalibration': 1,
                            'WaveUnit': ''
                        },
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 3,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高正常',
                                            'Value': 3
                                        },
                                        {
                                            'Code': 5,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低正常',
                                            'Value': -3.5
                                        },
                                        {
                                            'Code': 6,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低预警告',
                                            'Value': -6.4000000953674316
                                        },
                                        {
                                            'Code': 7,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低警告',
                                            'Value': -10
                                        },
                                        {
                                            'Code': 8,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低危险',
                                            'Value': -10
                                        }
                                    ],
                                    'Mode': [
                                        {
                                            'Code': 0,
                                            'Name': '自动'
                                        },
                                        {
                                            'Code': 1,
                                            'Name': '单值'
                                        },
                                        {
                                            'Code': 2,
                                            'Name': '曲线或曲面'
                                        }
                                    ],
                                    'ModeCode': 0,
                                    'Para': [
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        },
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        }
                                    ]
                                }
                            },
                            'BiasVoltHigh': 5,
                            'BiasVoltLow': 0,
                            'CHNum': 8,
                            'DelayAlarmTime': 10000,
                            'DisplacementCalibration': 1,
                            'DivFreInfo': [
                                {
                                    'AlarmStrategy': {
                                        'Absolute': {
                                            'Category': [
                                                {
                                                    'Code': 0,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高危险',
                                                    'Value': 15
                                                },
                                                {
                                                    'Code': 1,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高警告',
                                                    'Value': 10
                                                },
                                                {
                                                    'Code': 2,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高预警告',
                                                    'Value': 6
                                                },
                                                {
                                                    'Code': 4,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '正常',
                                                    'Value': 0
                                                }
                                            ]
                                        },
                                        'Comparative': {
                                            'IntevalTime': 10000,
                                            'IsAllow': false,
                                            'Para': [
                                                {
                                                    'CHNum': 0,
                                                    'CardNum': 1
                                                }
                                            ],
                                            'Percent': 0.20000000298023224,
                                            'Range': ''
                                        }
                                    },
                                    'BasedOnRPM': {
                                        'Code': 0,
                                        'MultiFre': 1,
                                        'Name': '基于转速'
                                    },
                                    'BasedOnRange': {
                                        'Code': 2,
                                        'FreHigh': 0,
                                        'FreLow': 0,
                                        'MaxFreNum': 0,
                                        'Name': '基于范围'
                                    },
                                    'Code': '',
                                    'Create_Time': '',
                                    'DivFreCode': -1,
                                    'FixedFre': {
                                        'CharacteristicFre': 80,
                                        'Code': 1,
                                        'Name': '固定频率',
                                        'Percent': 0.05000000074505806
                                    },
                                    'Guid': '',
                                    'Modify_Time': '',
                                    'Name': 'example',
                                    'Remarks': '',
                                    'T_Item_Code': '',
                                    'T_Item_Guid': '',
                                    'T_Item_Name': ''
                                }
                            ],
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsMultiplication': false,
                            'IsSaveWaveToSD': true,
                            'IsUploadData': false,
                            'IsUploadWave': true,
                            'IsValidWave': true,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'MultiplicationCor': 1,
                            'Organization': [
                            ],
                            'PRMSlotNum': 0,
                            'RPMCHNum': 0,
                            'RPMCardNum': 1,
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 0,
                                    'Name': '有效值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 1,
                                    'Name': '峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 2,
                                    'Name': '峰峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 3,
                                    'Name': '平均值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 0,
                            'Sensitivity': 0.096000000834465027,
                            'SubCHNum': 0,
                            'TPDirCategory': [
                                {
                                    'Code': 0,
                                    'Degree': 90,
                                    'Name': 'Radial direction'
                                },
                                {
                                    'Code': 1,
                                    'Degree': 0,
                                    'Name': 'Axial direction'
                                }
                            ],
                            'TPDirCode': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号8',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点8',
                            'Unit': '',
                            'VelocityCalibration': 1,
                            'WaveUnit': ''
                        },
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 3,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高正常',
                                            'Value': 3
                                        },
                                        {
                                            'Code': 5,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低正常',
                                            'Value': -3.5
                                        },
                                        {
                                            'Code': 6,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低预警告',
                                            'Value': -6.4000000953674316
                                        },
                                        {
                                            'Code': 7,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低警告',
                                            'Value': -10
                                        },
                                        {
                                            'Code': 8,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低危险',
                                            'Value': -10
                                        }
                                    ],
                                    'Mode': [
                                        {
                                            'Code': 0,
                                            'Name': '自动'
                                        },
                                        {
                                            'Code': 1,
                                            'Name': '单值'
                                        },
                                        {
                                            'Code': 2,
                                            'Name': '曲线或曲面'
                                        }
                                    ],
                                    'ModeCode': 0,
                                    'Para': [
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        },
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        }
                                    ]
                                }
                            },
                            'BiasVoltHigh': 5,
                            'BiasVoltLow': 0,
                            'CHNum': 9,
                            'DelayAlarmTime': 10000,
                            'DisplacementCalibration': 1,
                            'DivFreInfo': [
                                {
                                    'AlarmStrategy': {
                                        'Absolute': {
                                            'Category': [
                                                {
                                                    'Code': 0,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高危险',
                                                    'Value': 15
                                                },
                                                {
                                                    'Code': 1,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高警告',
                                                    'Value': 10
                                                },
                                                {
                                                    'Code': 2,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高预警告',
                                                    'Value': 6
                                                },
                                                {
                                                    'Code': 4,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '正常',
                                                    'Value': 0
                                                }
                                            ]
                                        },
                                        'Comparative': {
                                            'IntevalTime': 10000,
                                            'IsAllow': false,
                                            'Para': [
                                                {
                                                    'CHNum': 0,
                                                    'CardNum': 1
                                                }
                                            ],
                                            'Percent': 0.20000000298023224,
                                            'Range': ''
                                        }
                                    },
                                    'BasedOnRPM': {
                                        'Code': 0,
                                        'MultiFre': 1,
                                        'Name': '基于转速'
                                    },
                                    'BasedOnRange': {
                                        'Code': 2,
                                        'FreHigh': 0,
                                        'FreLow': 0,
                                        'MaxFreNum': 0,
                                        'Name': '基于范围'
                                    },
                                    'Code': '',
                                    'Create_Time': '',
                                    'DivFreCode': -1,
                                    'FixedFre': {
                                        'CharacteristicFre': 80,
                                        'Code': 1,
                                        'Name': '固定频率',
                                        'Percent': 0.05000000074505806
                                    },
                                    'Guid': '',
                                    'Modify_Time': '',
                                    'Name': 'example',
                                    'Remarks': '',
                                    'T_Item_Code': '',
                                    'T_Item_Guid': '',
                                    'T_Item_Name': ''
                                }
                            ],
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsMultiplication': false,
                            'IsSaveWaveToSD': true,
                            'IsUploadData': false,
                            'IsUploadWave': true,
                            'IsValidWave': true,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'MultiplicationCor': 1,
                            'Organization': [
                            ],
                            'PRMSlotNum': 0,
                            'RPMCHNum': 0,
                            'RPMCardNum': 1,
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 0,
                                    'Name': '有效值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 1,
                                    'Name': '峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 2,
                                    'Name': '峰峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 3,
                                    'Name': '平均值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 0,
                            'Sensitivity': 0.096000000834465027,
                            'SubCHNum': 0,
                            'TPDirCategory': [
                                {
                                    'Code': 0,
                                    'Degree': 90,
                                    'Name': 'Radial direction'
                                },
                                {
                                    'Code': 1,
                                    'Degree': 0,
                                    'Name': 'Axial direction'
                                }
                            ],
                            'TPDirCode': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号9',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点9',
                            'Unit': '',
                            'VelocityCalibration': 1,
                            'WaveUnit': ''
                        }
                    ]
                },
                'WorkTime': 2147483647
            },
            {
                'BatteryEnergy': 0.80000001192092896,
                'ExtraInfo': {
                },
                'Remarks': '',
                'SlaveIdentifier': 'FE010C05A2',
                'SleepTime': 0,
                'TransmissionName': '',
                'TransmissionType': 0,
                'Version': '',
                'WirelessScalarSlot': {
                    'SlotNum': 0,
                    'WirelessScalarChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 4,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '正常',
                                            'Value': 0
                                        }
                                    ]
                                }
                            },
                            'CHNum': 10,
                            'DelayAlarmTime': 10000,
                            'EquationCategory': [
                                {
                                    'CalibrationCor': 1,
                                    'Code': 0,
                                    'Formula': '',
                                    'Name': '一次线性方程'
                                },
                                {
                                    'CalibrationCor': 1,
                                    'Code': 1,
                                    'Formula': '',
                                    'Name': '一次非线性方程'
                                }
                            ],
                            'EquationCode': 0,
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsUploadData': false,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'Organization': [
                            ],
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 7,
                                    'Name': '数值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 7,
                            'SubCHNum': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号10',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点10',
                            'Unit': ''
                        }
                    ]
                },
                'WirelessVibrationSlot': {
                    'Integration': 1,
                    'SampleFreCategory': [
                        {
                            'Code': 7,
                            'Name': '338HZ'
                        },
                        {
                            'Code': 6,
                            'Name': '675HZ'
                        },
                        {
                            'Code': 5,
                            'Name': '1.35KHZ'
                        },
                        {
                            'Code': 4,
                            'Name': '2.7KHZ'
                        },
                        {
                            'Code': 3,
                            'Name': '5.4KHZ'
                        },
                        {
                            'Code': 2,
                            'Name': '10.8KHZ'
                        },
                        {
                            'Code': 1,
                            'Name': '21.6KHZ'
                        },
                        {
                            'Code': 0,
                            'Name': '43.2KHZ'
                        }
                    ],
                    'SampleFreCode': 4,
                    'SamplePointCategory': [
                        {
                            'Code': 0,
                            'Name': '256'
                        },
                        {
                            'Code': 1,
                            'Name': '512'
                        },
                        {
                            'Code': 2,
                            'Name': '1024'
                        },
                        {
                            'Code': 3,
                            'Name': '2048'
                        },
                        {
                            'Code': 4,
                            'Name': '4096'
                        },
                        {
                            'Code': 5,
                            'Name': '8192'
                        },
                        {
                            'Code': 6,
                            'Name': '16384'
                        }
                    ],
                    'SamplePointCode': 3,
                    'SlotNum': 1,
                    'Unit': 'mm/s',
                    'WirelessVibrationChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 3,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高正常',
                                            'Value': 3
                                        },
                                        {
                                            'Code': 5,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低正常',
                                            'Value': -3.5
                                        },
                                        {
                                            'Code': 6,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低预警告',
                                            'Value': -6.4000000953674316
                                        },
                                        {
                                            'Code': 7,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低警告',
                                            'Value': -10
                                        },
                                        {
                                            'Code': 8,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低危险',
                                            'Value': -10
                                        }
                                    ],
                                    'Mode': [
                                        {
                                            'Code': 0,
                                            'Name': '自动'
                                        },
                                        {
                                            'Code': 1,
                                            'Name': '单值'
                                        },
                                        {
                                            'Code': 2,
                                            'Name': '曲线或曲面'
                                        }
                                    ],
                                    'ModeCode': 0,
                                    'Para': [
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        },
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        }
                                    ]
                                }
                            },
                            'BiasVoltHigh': 5,
                            'BiasVoltLow': 0,
                            'CHNum': 11,
                            'DelayAlarmTime': 10000,
                            'DisplacementCalibration': 1,
                            'DivFreInfo': [
                                {
                                    'AlarmStrategy': {
                                        'Absolute': {
                                            'Category': [
                                                {
                                                    'Code': 0,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高危险',
                                                    'Value': 15
                                                },
                                                {
                                                    'Code': 1,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高警告',
                                                    'Value': 10
                                                },
                                                {
                                                    'Code': 2,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高预警告',
                                                    'Value': 6
                                                },
                                                {
                                                    'Code': 4,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '正常',
                                                    'Value': 0
                                                }
                                            ]
                                        },
                                        'Comparative': {
                                            'IntevalTime': 10000,
                                            'IsAllow': false,
                                            'Para': [
                                                {
                                                    'CHNum': 0,
                                                    'CardNum': 1
                                                }
                                            ],
                                            'Percent': 0.20000000298023224,
                                            'Range': ''
                                        }
                                    },
                                    'BasedOnRPM': {
                                        'Code': 0,
                                        'MultiFre': 1,
                                        'Name': '基于转速'
                                    },
                                    'BasedOnRange': {
                                        'Code': 2,
                                        'FreHigh': 0,
                                        'FreLow': 0,
                                        'MaxFreNum': 0,
                                        'Name': '基于范围'
                                    },
                                    'Code': '',
                                    'Create_Time': '',
                                    'DivFreCode': -1,
                                    'FixedFre': {
                                        'CharacteristicFre': 80,
                                        'Code': 1,
                                        'Name': '固定频率',
                                        'Percent': 0.05000000074505806
                                    },
                                    'Guid': '',
                                    'Modify_Time': '',
                                    'Name': 'example',
                                    'Remarks': '',
                                    'T_Item_Code': '',
                                    'T_Item_Guid': '',
                                    'T_Item_Name': ''
                                }
                            ],
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsMultiplication': false,
                            'IsSaveWaveToSD': true,
                            'IsUploadData': false,
                            'IsUploadWave': true,
                            'IsValidWave': true,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'MultiplicationCor': 1,
                            'Organization': [
                            ],
                            'PRMSlotNum': 0,
                            'RPMCHNum': 0,
                            'RPMCardNum': 1,
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 0,
                                    'Name': '有效值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 1,
                                    'Name': '峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 2,
                                    'Name': '峰峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 3,
                                    'Name': '平均值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 0,
                            'Sensitivity': 0.096000000834465027,
                            'SubCHNum': 0,
                            'TPDirCategory': [
                                {
                                    'Code': 0,
                                    'Degree': 90,
                                    'Name': 'Radial direction'
                                },
                                {
                                    'Code': 1,
                                    'Degree': 0,
                                    'Name': 'Axial direction'
                                }
                            ],
                            'TPDirCode': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号11',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点11',
                            'Unit': '',
                            'VelocityCalibration': 1,
                            'WaveUnit': ''
                        },
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 3,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高正常',
                                            'Value': 3
                                        },
                                        {
                                            'Code': 5,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低正常',
                                            'Value': -3.5
                                        },
                                        {
                                            'Code': 6,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低预警告',
                                            'Value': -6.4000000953674316
                                        },
                                        {
                                            'Code': 7,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低警告',
                                            'Value': -10
                                        },
                                        {
                                            'Code': 8,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低危险',
                                            'Value': -10
                                        }
                                    ],
                                    'Mode': [
                                        {
                                            'Code': 0,
                                            'Name': '自动'
                                        },
                                        {
                                            'Code': 1,
                                            'Name': '单值'
                                        },
                                        {
                                            'Code': 2,
                                            'Name': '曲线或曲面'
                                        }
                                    ],
                                    'ModeCode': 0,
                                    'Para': [
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        },
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        }
                                    ]
                                }
                            },
                            'BiasVoltHigh': 5,
                            'BiasVoltLow': 0,
                            'CHNum': 12,
                            'DelayAlarmTime': 10000,
                            'DisplacementCalibration': 1,
                            'DivFreInfo': [
                                {
                                    'AlarmStrategy': {
                                        'Absolute': {
                                            'Category': [
                                                {
                                                    'Code': 0,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高危险',
                                                    'Value': 15
                                                },
                                                {
                                                    'Code': 1,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高警告',
                                                    'Value': 10
                                                },
                                                {
                                                    'Code': 2,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高预警告',
                                                    'Value': 6
                                                },
                                                {
                                                    'Code': 4,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '正常',
                                                    'Value': 0
                                                }
                                            ]
                                        },
                                        'Comparative': {
                                            'IntevalTime': 10000,
                                            'IsAllow': false,
                                            'Para': [
                                                {
                                                    'CHNum': 0,
                                                    'CardNum': 1
                                                }
                                            ],
                                            'Percent': 0.20000000298023224,
                                            'Range': ''
                                        }
                                    },
                                    'BasedOnRPM': {
                                        'Code': 0,
                                        'MultiFre': 1,
                                        'Name': '基于转速'
                                    },
                                    'BasedOnRange': {
                                        'Code': 2,
                                        'FreHigh': 0,
                                        'FreLow': 0,
                                        'MaxFreNum': 0,
                                        'Name': '基于范围'
                                    },
                                    'Code': '',
                                    'Create_Time': '',
                                    'DivFreCode': -1,
                                    'FixedFre': {
                                        'CharacteristicFre': 80,
                                        'Code': 1,
                                        'Name': '固定频率',
                                        'Percent': 0.05000000074505806
                                    },
                                    'Guid': '',
                                    'Modify_Time': '',
                                    'Name': 'example',
                                    'Remarks': '',
                                    'T_Item_Code': '',
                                    'T_Item_Guid': '',
                                    'T_Item_Name': ''
                                }
                            ],
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsMultiplication': false,
                            'IsSaveWaveToSD': true,
                            'IsUploadData': false,
                            'IsUploadWave': true,
                            'IsValidWave': true,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'MultiplicationCor': 1,
                            'Organization': [
                            ],
                            'PRMSlotNum': 0,
                            'RPMCHNum': 0,
                            'RPMCardNum': 1,
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 0,
                                    'Name': '有效值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 1,
                                    'Name': '峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 2,
                                    'Name': '峰峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 3,
                                    'Name': '平均值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 0,
                            'Sensitivity': 0.096000000834465027,
                            'SubCHNum': 0,
                            'TPDirCategory': [
                                {
                                    'Code': 0,
                                    'Degree': 90,
                                    'Name': 'Radial direction'
                                },
                                {
                                    'Code': 1,
                                    'Degree': 0,
                                    'Name': 'Axial direction'
                                }
                            ],
                            'TPDirCode': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号12',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点12',
                            'Unit': '',
                            'VelocityCalibration': 1,
                            'WaveUnit': ''
                        },
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 3,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高正常',
                                            'Value': 3
                                        },
                                        {
                                            'Code': 5,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低正常',
                                            'Value': -3.5
                                        },
                                        {
                                            'Code': 6,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低预警告',
                                            'Value': -6.4000000953674316
                                        },
                                        {
                                            'Code': 7,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低警告',
                                            'Value': -10
                                        },
                                        {
                                            'Code': 8,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低危险',
                                            'Value': -10
                                        }
                                    ],
                                    'Mode': [
                                        {
                                            'Code': 0,
                                            'Name': '自动'
                                        },
                                        {
                                            'Code': 1,
                                            'Name': '单值'
                                        },
                                        {
                                            'Code': 2,
                                            'Name': '曲线或曲面'
                                        }
                                    ],
                                    'ModeCode': 0,
                                    'Para': [
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        },
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        }
                                    ]
                                }
                            },
                            'BiasVoltHigh': 5,
                            'BiasVoltLow': 0,
                            'CHNum': 13,
                            'DelayAlarmTime': 10000,
                            'DisplacementCalibration': 1,
                            'DivFreInfo': [
                                {
                                    'AlarmStrategy': {
                                        'Absolute': {
                                            'Category': [
                                                {
                                                    'Code': 0,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高危险',
                                                    'Value': 15
                                                },
                                                {
                                                    'Code': 1,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高警告',
                                                    'Value': 10
                                                },
                                                {
                                                    'Code': 2,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高预警告',
                                                    'Value': 6
                                                },
                                                {
                                                    'Code': 4,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '正常',
                                                    'Value': 0
                                                }
                                            ]
                                        },
                                        'Comparative': {
                                            'IntevalTime': 10000,
                                            'IsAllow': false,
                                            'Para': [
                                                {
                                                    'CHNum': 0,
                                                    'CardNum': 1
                                                }
                                            ],
                                            'Percent': 0.20000000298023224,
                                            'Range': ''
                                        }
                                    },
                                    'BasedOnRPM': {
                                        'Code': 0,
                                        'MultiFre': 1,
                                        'Name': '基于转速'
                                    },
                                    'BasedOnRange': {
                                        'Code': 2,
                                        'FreHigh': 0,
                                        'FreLow': 0,
                                        'MaxFreNum': 0,
                                        'Name': '基于范围'
                                    },
                                    'Code': '',
                                    'Create_Time': '',
                                    'DivFreCode': -1,
                                    'FixedFre': {
                                        'CharacteristicFre': 80,
                                        'Code': 1,
                                        'Name': '固定频率',
                                        'Percent': 0.05000000074505806
                                    },
                                    'Guid': '',
                                    'Modify_Time': '',
                                    'Name': 'example',
                                    'Remarks': '',
                                    'T_Item_Code': '',
                                    'T_Item_Guid': '',
                                    'T_Item_Name': ''
                                }
                            ],
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsMultiplication': false,
                            'IsSaveWaveToSD': true,
                            'IsUploadData': false,
                            'IsUploadWave': true,
                            'IsValidWave': true,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'MultiplicationCor': 1,
                            'Organization': [
                            ],
                            'PRMSlotNum': 0,
                            'RPMCHNum': 0,
                            'RPMCardNum': 1,
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 0,
                                    'Name': '有效值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 1,
                                    'Name': '峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 2,
                                    'Name': '峰峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 3,
                                    'Name': '平均值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 0,
                            'Sensitivity': 0.096000000834465027,
                            'SubCHNum': 0,
                            'TPDirCategory': [
                                {
                                    'Code': 0,
                                    'Degree': 90,
                                    'Name': 'Radial direction'
                                },
                                {
                                    'Code': 1,
                                    'Degree': 0,
                                    'Name': 'Axial direction'
                                }
                            ],
                            'TPDirCode': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号13',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点13',
                            'Unit': '',
                            'VelocityCalibration': 1,
                            'WaveUnit': ''
                        }
                    ]
                },
                'WorkTime': 2147483647
            },
            {
                'BatteryEnergy': 0.80000001192092896,
                'ExtraInfo': {
                },
                'Remarks': '',
                'SlaveIdentifier': 'FE010C05A3',
                'SleepTime': 0,
                'TransmissionName': '',
                'TransmissionType': 0,
                'Version': '',
                'WirelessScalarSlot': {
                    'SlotNum': 0,
                    'WirelessScalarChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 4,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '正常',
                                            'Value': 0
                                        }
                                    ]
                                }
                            },
                            'CHNum': 14,
                            'DelayAlarmTime': 10000,
                            'EquationCategory': [
                                {
                                    'CalibrationCor': 1,
                                    'Code': 0,
                                    'Formula': '',
                                    'Name': '一次线性方程'
                                },
                                {
                                    'CalibrationCor': 1,
                                    'Code': 1,
                                    'Formula': '',
                                    'Name': '一次非线性方程'
                                }
                            ],
                            'EquationCode': 0,
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsUploadData': false,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'Organization': [
                            ],
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 7,
                                    'Name': '数值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 7,
                            'SubCHNum': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号14',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点14',
                            'Unit': ''
                        }
                    ]
                },
                'WirelessVibrationSlot': {
                    'Integration': 1,
                    'SampleFreCategory': [
                        {
                            'Code': 7,
                            'Name': '338HZ'
                        },
                        {
                            'Code': 6,
                            'Name': '675HZ'
                        },
                        {
                            'Code': 5,
                            'Name': '1.35KHZ'
                        },
                        {
                            'Code': 4,
                            'Name': '2.7KHZ'
                        },
                        {
                            'Code': 3,
                            'Name': '5.4KHZ'
                        },
                        {
                            'Code': 2,
                            'Name': '10.8KHZ'
                        },
                        {
                            'Code': 1,
                            'Name': '21.6KHZ'
                        },
                        {
                            'Code': 0,
                            'Name': '43.2KHZ'
                        }
                    ],
                    'SampleFreCode': 4,
                    'SamplePointCategory': [
                        {
                            'Code': 0,
                            'Name': '256'
                        },
                        {
                            'Code': 1,
                            'Name': '512'
                        },
                        {
                            'Code': 2,
                            'Name': '1024'
                        },
                        {
                            'Code': 3,
                            'Name': '2048'
                        },
                        {
                            'Code': 4,
                            'Name': '4096'
                        },
                        {
                            'Code': 5,
                            'Name': '8192'
                        },
                        {
                            'Code': 6,
                            'Name': '16384'
                        }
                    ],
                    'SamplePointCode': 3,
                    'SlotNum': 1,
                    'Unit': 'mm/s',
                    'WirelessVibrationChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 3,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高正常',
                                            'Value': 3
                                        },
                                        {
                                            'Code': 5,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低正常',
                                            'Value': -3.5
                                        },
                                        {
                                            'Code': 6,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低预警告',
                                            'Value': -6.4000000953674316
                                        },
                                        {
                                            'Code': 7,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低警告',
                                            'Value': -10
                                        },
                                        {
                                            'Code': 8,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低危险',
                                            'Value': -10
                                        }
                                    ],
                                    'Mode': [
                                        {
                                            'Code': 0,
                                            'Name': '自动'
                                        },
                                        {
                                            'Code': 1,
                                            'Name': '单值'
                                        },
                                        {
                                            'Code': 2,
                                            'Name': '曲线或曲面'
                                        }
                                    ],
                                    'ModeCode': 0,
                                    'Para': [
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        },
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        }
                                    ]
                                }
                            },
                            'BiasVoltHigh': 5,
                            'BiasVoltLow': 0,
                            'CHNum': 15,
                            'DelayAlarmTime': 10000,
                            'DisplacementCalibration': 1,
                            'DivFreInfo': [
                                {
                                    'AlarmStrategy': {
                                        'Absolute': {
                                            'Category': [
                                                {
                                                    'Code': 0,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高危险',
                                                    'Value': 15
                                                },
                                                {
                                                    'Code': 1,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高警告',
                                                    'Value': 10
                                                },
                                                {
                                                    'Code': 2,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高预警告',
                                                    'Value': 6
                                                },
                                                {
                                                    'Code': 4,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '正常',
                                                    'Value': 0
                                                }
                                            ]
                                        },
                                        'Comparative': {
                                            'IntevalTime': 10000,
                                            'IsAllow': false,
                                            'Para': [
                                                {
                                                    'CHNum': 0,
                                                    'CardNum': 1
                                                }
                                            ],
                                            'Percent': 0.20000000298023224,
                                            'Range': ''
                                        }
                                    },
                                    'BasedOnRPM': {
                                        'Code': 0,
                                        'MultiFre': 1,
                                        'Name': '基于转速'
                                    },
                                    'BasedOnRange': {
                                        'Code': 2,
                                        'FreHigh': 0,
                                        'FreLow': 0,
                                        'MaxFreNum': 0,
                                        'Name': '基于范围'
                                    },
                                    'Code': '',
                                    'Create_Time': '',
                                    'DivFreCode': -1,
                                    'FixedFre': {
                                        'CharacteristicFre': 80,
                                        'Code': 1,
                                        'Name': '固定频率',
                                        'Percent': 0.05000000074505806
                                    },
                                    'Guid': '',
                                    'Modify_Time': '',
                                    'Name': 'example',
                                    'Remarks': '',
                                    'T_Item_Code': '',
                                    'T_Item_Guid': '',
                                    'T_Item_Name': ''
                                }
                            ],
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsMultiplication': false,
                            'IsSaveWaveToSD': true,
                            'IsUploadData': false,
                            'IsUploadWave': true,
                            'IsValidWave': true,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'MultiplicationCor': 1,
                            'Organization': [
                            ],
                            'PRMSlotNum': 0,
                            'RPMCHNum': 0,
                            'RPMCardNum': 1,
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 0,
                                    'Name': '有效值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 1,
                                    'Name': '峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 2,
                                    'Name': '峰峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 3,
                                    'Name': '平均值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 0,
                            'Sensitivity': 0.096000000834465027,
                            'SubCHNum': 0,
                            'TPDirCategory': [
                                {
                                    'Code': 0,
                                    'Degree': 90,
                                    'Name': 'Radial direction'
                                },
                                {
                                    'Code': 1,
                                    'Degree': 0,
                                    'Name': 'Axial direction'
                                }
                            ],
                            'TPDirCode': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号15',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点15',
                            'Unit': '',
                            'VelocityCalibration': 1,
                            'WaveUnit': ''
                        }
                    ]
                },
                'WorkTime': 2147483647
            },
            {
                'BatteryEnergy': 0.80000001192092896,
                'ExtraInfo': {
                },
                'Remarks': '',
                'SlaveIdentifier': 'FE010C05A4',
                'SleepTime': 0,
                'TransmissionName': '',
                'TransmissionType': 0,
                'Version': '',
                'WirelessScalarSlot': {
                    'SlotNum': 0,
                    'WirelessScalarChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 4,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '正常',
                                            'Value': 0
                                        }
                                    ]
                                }
                            },
                            'CHNum': 16,
                            'DelayAlarmTime': 10000,
                            'EquationCategory': [
                                {
                                    'CalibrationCor': 1,
                                    'Code': 0,
                                    'Formula': '',
                                    'Name': '一次线性方程'
                                },
                                {
                                    'CalibrationCor': 1,
                                    'Code': 1,
                                    'Formula': '',
                                    'Name': '一次非线性方程'
                                }
                            ],
                            'EquationCode': 0,
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsUploadData': false,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'Organization': [
                            ],
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 7,
                                    'Name': '数值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 7,
                            'SubCHNum': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号16',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点16',
                            'Unit': ''
                        }
                    ]
                },
                'WirelessVibrationSlot': {
                    'Integration': 1,
                    'SampleFreCategory': [
                        {
                            'Code': 7,
                            'Name': '338HZ'
                        },
                        {
                            'Code': 6,
                            'Name': '675HZ'
                        },
                        {
                            'Code': 5,
                            'Name': '1.35KHZ'
                        },
                        {
                            'Code': 4,
                            'Name': '2.7KHZ'
                        },
                        {
                            'Code': 3,
                            'Name': '5.4KHZ'
                        },
                        {
                            'Code': 2,
                            'Name': '10.8KHZ'
                        },
                        {
                            'Code': 1,
                            'Name': '21.6KHZ'
                        },
                        {
                            'Code': 0,
                            'Name': '43.2KHZ'
                        }
                    ],
                    'SampleFreCode': 4,
                    'SamplePointCategory': [
                        {
                            'Code': 0,
                            'Name': '256'
                        },
                        {
                            'Code': 1,
                            'Name': '512'
                        },
                        {
                            'Code': 2,
                            'Name': '1024'
                        },
                        {
                            'Code': 3,
                            'Name': '2048'
                        },
                        {
                            'Code': 4,
                            'Name': '4096'
                        },
                        {
                            'Code': 5,
                            'Name': '8192'
                        },
                        {
                            'Code': 6,
                            'Name': '16384'
                        }
                    ],
                    'SamplePointCode': 3,
                    'SlotNum': 1,
                    'Unit': 'mm/s',
                    'WirelessVibrationChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 3,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高正常',
                                            'Value': 3
                                        },
                                        {
                                            'Code': 5,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低正常',
                                            'Value': -3.5
                                        },
                                        {
                                            'Code': 6,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低预警告',
                                            'Value': -6.4000000953674316
                                        },
                                        {
                                            'Code': 7,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低警告',
                                            'Value': -10
                                        },
                                        {
                                            'Code': 8,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低危险',
                                            'Value': -10
                                        }
                                    ],
                                    'Mode': [
                                        {
                                            'Code': 0,
                                            'Name': '自动'
                                        },
                                        {
                                            'Code': 1,
                                            'Name': '单值'
                                        },
                                        {
                                            'Code': 2,
                                            'Name': '曲线或曲面'
                                        }
                                    ],
                                    'ModeCode': 0,
                                    'Para': [
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        },
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        }
                                    ]
                                }
                            },
                            'BiasVoltHigh': 5,
                            'BiasVoltLow': 0,
                            'CHNum': 17,
                            'DelayAlarmTime': 10000,
                            'DisplacementCalibration': 1,
                            'DivFreInfo': [
                                {
                                    'AlarmStrategy': {
                                        'Absolute': {
                                            'Category': [
                                                {
                                                    'Code': 0,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高危险',
                                                    'Value': 15
                                                },
                                                {
                                                    'Code': 1,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高警告',
                                                    'Value': 10
                                                },
                                                {
                                                    'Code': 2,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高预警告',
                                                    'Value': 6
                                                },
                                                {
                                                    'Code': 4,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '正常',
                                                    'Value': 0
                                                }
                                            ]
                                        },
                                        'Comparative': {
                                            'IntevalTime': 10000,
                                            'IsAllow': false,
                                            'Para': [
                                                {
                                                    'CHNum': 0,
                                                    'CardNum': 1
                                                }
                                            ],
                                            'Percent': 0.20000000298023224,
                                            'Range': ''
                                        }
                                    },
                                    'BasedOnRPM': {
                                        'Code': 0,
                                        'MultiFre': 1,
                                        'Name': '基于转速'
                                    },
                                    'BasedOnRange': {
                                        'Code': 2,
                                        'FreHigh': 0,
                                        'FreLow': 0,
                                        'MaxFreNum': 0,
                                        'Name': '基于范围'
                                    },
                                    'Code': '',
                                    'Create_Time': '',
                                    'DivFreCode': -1,
                                    'FixedFre': {
                                        'CharacteristicFre': 80,
                                        'Code': 1,
                                        'Name': '固定频率',
                                        'Percent': 0.05000000074505806
                                    },
                                    'Guid': '',
                                    'Modify_Time': '',
                                    'Name': 'example',
                                    'Remarks': '',
                                    'T_Item_Code': '',
                                    'T_Item_Guid': '',
                                    'T_Item_Name': ''
                                }
                            ],
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsMultiplication': false,
                            'IsSaveWaveToSD': true,
                            'IsUploadData': false,
                            'IsUploadWave': true,
                            'IsValidWave': true,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'MultiplicationCor': 1,
                            'Organization': [
                            ],
                            'PRMSlotNum': 0,
                            'RPMCHNum': 0,
                            'RPMCardNum': 1,
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 0,
                                    'Name': '有效值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 1,
                                    'Name': '峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 2,
                                    'Name': '峰峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 3,
                                    'Name': '平均值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 0,
                            'Sensitivity': 0.096000000834465027,
                            'SubCHNum': 0,
                            'TPDirCategory': [
                                {
                                    'Code': 0,
                                    'Degree': 90,
                                    'Name': 'Radial direction'
                                },
                                {
                                    'Code': 1,
                                    'Degree': 0,
                                    'Name': 'Axial direction'
                                }
                            ],
                            'TPDirCode': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号17',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点17',
                            'Unit': '',
                            'VelocityCalibration': 1,
                            'WaveUnit': ''
                        }
                    ]
                },
                'WorkTime': 2147483647
            },
            {
                'BatteryEnergy': 0.80000001192092896,
                'ExtraInfo': {
                },
                'Remarks': '',
                'SlaveIdentifier': 'FE010C05A5',
                'SleepTime': 0,
                'TransmissionName': '',
                'TransmissionType': 0,
                'Version': '',
                'WirelessScalarSlot': {
                    'SlotNum': 0,
                    'WirelessScalarChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 4,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '正常',
                                            'Value': 0
                                        }
                                    ]
                                }
                            },
                            'CHNum': 18,
                            'DelayAlarmTime': 10000,
                            'EquationCategory': [
                                {
                                    'CalibrationCor': 1,
                                    'Code': 0,
                                    'Formula': '',
                                    'Name': '一次线性方程'
                                },
                                {
                                    'CalibrationCor': 1,
                                    'Code': 1,
                                    'Formula': '',
                                    'Name': '一次非线性方程'
                                }
                            ],
                            'EquationCode': 0,
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsUploadData': false,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'Organization': [
                            ],
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 7,
                                    'Name': '数值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 7,
                            'SubCHNum': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号18',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点18',
                            'Unit': ''
                        }
                    ]
                },
                'WirelessVibrationSlot': {
                    'Integration': 1,
                    'SampleFreCategory': [
                        {
                            'Code': 7,
                            'Name': '338HZ'
                        },
                        {
                            'Code': 6,
                            'Name': '675HZ'
                        },
                        {
                            'Code': 5,
                            'Name': '1.35KHZ'
                        },
                        {
                            'Code': 4,
                            'Name': '2.7KHZ'
                        },
                        {
                            'Code': 3,
                            'Name': '5.4KHZ'
                        },
                        {
                            'Code': 2,
                            'Name': '10.8KHZ'
                        },
                        {
                            'Code': 1,
                            'Name': '21.6KHZ'
                        },
                        {
                            'Code': 0,
                            'Name': '43.2KHZ'
                        }
                    ],
                    'SampleFreCode': 4,
                    'SamplePointCategory': [
                        {
                            'Code': 0,
                            'Name': '256'
                        },
                        {
                            'Code': 1,
                            'Name': '512'
                        },
                        {
                            'Code': 2,
                            'Name': '1024'
                        },
                        {
                            'Code': 3,
                            'Name': '2048'
                        },
                        {
                            'Code': 4,
                            'Name': '4096'
                        },
                        {
                            'Code': 5,
                            'Name': '8192'
                        },
                        {
                            'Code': 6,
                            'Name': '16384'
                        }
                    ],
                    'SamplePointCode': 3,
                    'SlotNum': 1,
                    'Unit': 'mm/s',
                    'WirelessVibrationChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 3,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高正常',
                                            'Value': 3
                                        },
                                        {
                                            'Code': 5,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低正常',
                                            'Value': -3.5
                                        },
                                        {
                                            'Code': 6,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低预警告',
                                            'Value': -6.4000000953674316
                                        },
                                        {
                                            'Code': 7,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低警告',
                                            'Value': -10
                                        },
                                        {
                                            'Code': 8,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低危险',
                                            'Value': -10
                                        }
                                    ],
                                    'Mode': [
                                        {
                                            'Code': 0,
                                            'Name': '自动'
                                        },
                                        {
                                            'Code': 1,
                                            'Name': '单值'
                                        },
                                        {
                                            'Code': 2,
                                            'Name': '曲线或曲面'
                                        }
                                    ],
                                    'ModeCode': 0,
                                    'Para': [
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        },
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        }
                                    ]
                                }
                            },
                            'BiasVoltHigh': 5,
                            'BiasVoltLow': 0,
                            'CHNum': 19,
                            'DelayAlarmTime': 10000,
                            'DisplacementCalibration': 1,
                            'DivFreInfo': [
                                {
                                    'AlarmStrategy': {
                                        'Absolute': {
                                            'Category': [
                                                {
                                                    'Code': 0,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高危险',
                                                    'Value': 15
                                                },
                                                {
                                                    'Code': 1,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高警告',
                                                    'Value': 10
                                                },
                                                {
                                                    'Code': 2,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高预警告',
                                                    'Value': 6
                                                },
                                                {
                                                    'Code': 4,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '正常',
                                                    'Value': 0
                                                }
                                            ]
                                        },
                                        'Comparative': {
                                            'IntevalTime': 10000,
                                            'IsAllow': false,
                                            'Para': [
                                                {
                                                    'CHNum': 0,
                                                    'CardNum': 1
                                                }
                                            ],
                                            'Percent': 0.20000000298023224,
                                            'Range': ''
                                        }
                                    },
                                    'BasedOnRPM': {
                                        'Code': 0,
                                        'MultiFre': 1,
                                        'Name': '基于转速'
                                    },
                                    'BasedOnRange': {
                                        'Code': 2,
                                        'FreHigh': 0,
                                        'FreLow': 0,
                                        'MaxFreNum': 0,
                                        'Name': '基于范围'
                                    },
                                    'Code': '',
                                    'Create_Time': '',
                                    'DivFreCode': -1,
                                    'FixedFre': {
                                        'CharacteristicFre': 80,
                                        'Code': 1,
                                        'Name': '固定频率',
                                        'Percent': 0.05000000074505806
                                    },
                                    'Guid': '',
                                    'Modify_Time': '',
                                    'Name': 'example',
                                    'Remarks': '',
                                    'T_Item_Code': '',
                                    'T_Item_Guid': '',
                                    'T_Item_Name': ''
                                }
                            ],
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsMultiplication': false,
                            'IsSaveWaveToSD': true,
                            'IsUploadData': false,
                            'IsUploadWave': true,
                            'IsValidWave': true,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'MultiplicationCor': 1,
                            'Organization': [
                            ],
                            'PRMSlotNum': 0,
                            'RPMCHNum': 0,
                            'RPMCardNum': 1,
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 0,
                                    'Name': '有效值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 1,
                                    'Name': '峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 2,
                                    'Name': '峰峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 3,
                                    'Name': '平均值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 0,
                            'Sensitivity': 0.096000000834465027,
                            'SubCHNum': 0,
                            'TPDirCategory': [
                                {
                                    'Code': 0,
                                    'Degree': 90,
                                    'Name': 'Radial direction'
                                },
                                {
                                    'Code': 1,
                                    'Degree': 0,
                                    'Name': 'Axial direction'
                                }
                            ],
                            'TPDirCode': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号19',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点19',
                            'Unit': '',
                            'VelocityCalibration': 1,
                            'WaveUnit': ''
                        }
                    ]
                },
                'WorkTime': 2147483647
            },
            {
                'BatteryEnergy': 0.80000001192092896,
                'ExtraInfo': {
                },
                'Remarks': '',
                'SlaveIdentifier': 'FE010C05A6',
                'SleepTime': 0,
                'TransmissionName': '',
                'TransmissionType': 0,
                'Version': '',
                'WirelessScalarSlot': {
                    'SlotNum': 0,
                    'WirelessScalarChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 4,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '正常',
                                            'Value': 0
                                        }
                                    ]
                                }
                            },
                            'CHNum': 20,
                            'DelayAlarmTime': 10000,
                            'EquationCategory': [
                                {
                                    'CalibrationCor': 1,
                                    'Code': 0,
                                    'Formula': '',
                                    'Name': '一次线性方程'
                                },
                                {
                                    'CalibrationCor': 1,
                                    'Code': 1,
                                    'Formula': '',
                                    'Name': '一次非线性方程'
                                }
                            ],
                            'EquationCode': 0,
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsUploadData': false,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'Organization': [
                            ],
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 7,
                                    'Name': '数值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 7,
                            'SubCHNum': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号20',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点20',
                            'Unit': ''
                        }
                    ]
                },
                'WirelessVibrationSlot': {
                    'Integration': 1,
                    'SampleFreCategory': [
                        {
                            'Code': 7,
                            'Name': '338HZ'
                        },
                        {
                            'Code': 6,
                            'Name': '675HZ'
                        },
                        {
                            'Code': 5,
                            'Name': '1.35KHZ'
                        },
                        {
                            'Code': 4,
                            'Name': '2.7KHZ'
                        },
                        {
                            'Code': 3,
                            'Name': '5.4KHZ'
                        },
                        {
                            'Code': 2,
                            'Name': '10.8KHZ'
                        },
                        {
                            'Code': 1,
                            'Name': '21.6KHZ'
                        },
                        {
                            'Code': 0,
                            'Name': '43.2KHZ'
                        }
                    ],
                    'SampleFreCode': 4,
                    'SamplePointCategory': [
                        {
                            'Code': 0,
                            'Name': '256'
                        },
                        {
                            'Code': 1,
                            'Name': '512'
                        },
                        {
                            'Code': 2,
                            'Name': '1024'
                        },
                        {
                            'Code': 3,
                            'Name': '2048'
                        },
                        {
                            'Code': 4,
                            'Name': '4096'
                        },
                        {
                            'Code': 5,
                            'Name': '8192'
                        },
                        {
                            'Code': 6,
                            'Name': '16384'
                        }
                    ],
                    'SamplePointCode': 3,
                    'SlotNum': 1,
                    'Unit': 'mm/s',
                    'WirelessVibrationChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 3,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高正常',
                                            'Value': 3
                                        },
                                        {
                                            'Code': 5,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低正常',
                                            'Value': -3.5
                                        },
                                        {
                                            'Code': 6,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低预警告',
                                            'Value': -6.4000000953674316
                                        },
                                        {
                                            'Code': 7,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低警告',
                                            'Value': -10
                                        },
                                        {
                                            'Code': 8,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低危险',
                                            'Value': -10
                                        }
                                    ],
                                    'Mode': [
                                        {
                                            'Code': 0,
                                            'Name': '自动'
                                        },
                                        {
                                            'Code': 1,
                                            'Name': '单值'
                                        },
                                        {
                                            'Code': 2,
                                            'Name': '曲线或曲面'
                                        }
                                    ],
                                    'ModeCode': 0,
                                    'Para': [
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        },
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        }
                                    ]
                                }
                            },
                            'BiasVoltHigh': 5,
                            'BiasVoltLow': 0,
                            'CHNum': 21,
                            'DelayAlarmTime': 10000,
                            'DisplacementCalibration': 1,
                            'DivFreInfo': [
                                {
                                    'AlarmStrategy': {
                                        'Absolute': {
                                            'Category': [
                                                {
                                                    'Code': 0,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高危险',
                                                    'Value': 15
                                                },
                                                {
                                                    'Code': 1,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高警告',
                                                    'Value': 10
                                                },
                                                {
                                                    'Code': 2,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高预警告',
                                                    'Value': 6
                                                },
                                                {
                                                    'Code': 4,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '正常',
                                                    'Value': 0
                                                }
                                            ]
                                        },
                                        'Comparative': {
                                            'IntevalTime': 10000,
                                            'IsAllow': false,
                                            'Para': [
                                                {
                                                    'CHNum': 0,
                                                    'CardNum': 1
                                                }
                                            ],
                                            'Percent': 0.20000000298023224,
                                            'Range': ''
                                        }
                                    },
                                    'BasedOnRPM': {
                                        'Code': 0,
                                        'MultiFre': 1,
                                        'Name': '基于转速'
                                    },
                                    'BasedOnRange': {
                                        'Code': 2,
                                        'FreHigh': 0,
                                        'FreLow': 0,
                                        'MaxFreNum': 0,
                                        'Name': '基于范围'
                                    },
                                    'Code': '',
                                    'Create_Time': '',
                                    'DivFreCode': -1,
                                    'FixedFre': {
                                        'CharacteristicFre': 80,
                                        'Code': 1,
                                        'Name': '固定频率',
                                        'Percent': 0.05000000074505806
                                    },
                                    'Guid': '',
                                    'Modify_Time': '',
                                    'Name': 'example',
                                    'Remarks': '',
                                    'T_Item_Code': '',
                                    'T_Item_Guid': '',
                                    'T_Item_Name': ''
                                }
                            ],
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsMultiplication': false,
                            'IsSaveWaveToSD': true,
                            'IsUploadData': false,
                            'IsUploadWave': true,
                            'IsValidWave': true,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'MultiplicationCor': 1,
                            'Organization': [
                            ],
                            'PRMSlotNum': 0,
                            'RPMCHNum': 0,
                            'RPMCardNum': 1,
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 0,
                                    'Name': '有效值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 1,
                                    'Name': '峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 2,
                                    'Name': '峰峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 3,
                                    'Name': '平均值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 0,
                            'Sensitivity': 0.096000000834465027,
                            'SubCHNum': 0,
                            'TPDirCategory': [
                                {
                                    'Code': 0,
                                    'Degree': 90,
                                    'Name': 'Radial direction'
                                },
                                {
                                    'Code': 1,
                                    'Degree': 0,
                                    'Name': 'Axial direction'
                                }
                            ],
                            'TPDirCode': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号21',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点21',
                            'Unit': '',
                            'VelocityCalibration': 1,
                            'WaveUnit': ''
                        }
                    ]
                },
                'WorkTime': 2147483647
            },
            {
                'BatteryEnergy': 0.80000001192092896,
                'ExtraInfo': {
                },
                'Remarks': '',
                'SlaveIdentifier': 'FE010C05A7',
                'SleepTime': 0,
                'TransmissionName': '',
                'TransmissionType': 0,
                'Version': '',
                'WirelessScalarSlot': {
                    'SlotNum': 0,
                    'WirelessScalarChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 4,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '正常',
                                            'Value': 0
                                        }
                                    ]
                                }
                            },
                            'CHNum': 22,
                            'DelayAlarmTime': 10000,
                            'EquationCategory': [
                                {
                                    'CalibrationCor': 1,
                                    'Code': 0,
                                    'Formula': '',
                                    'Name': '一次线性方程'
                                },
                                {
                                    'CalibrationCor': 1,
                                    'Code': 1,
                                    'Formula': '',
                                    'Name': '一次非线性方程'
                                }
                            ],
                            'EquationCode': 0,
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsUploadData': false,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'Organization': [
                            ],
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 7,
                                    'Name': '数值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 7,
                            'SubCHNum': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号22',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点22',
                            'Unit': ''
                        }
                    ]
                },
                'WirelessVibrationSlot': {
                    'Integration': 1,
                    'SampleFreCategory': [
                        {
                            'Code': 7,
                            'Name': '338HZ'
                        },
                        {
                            'Code': 6,
                            'Name': '675HZ'
                        },
                        {
                            'Code': 5,
                            'Name': '1.35KHZ'
                        },
                        {
                            'Code': 4,
                            'Name': '2.7KHZ'
                        },
                        {
                            'Code': 3,
                            'Name': '5.4KHZ'
                        },
                        {
                            'Code': 2,
                            'Name': '10.8KHZ'
                        },
                        {
                            'Code': 1,
                            'Name': '21.6KHZ'
                        },
                        {
                            'Code': 0,
                            'Name': '43.2KHZ'
                        }
                    ],
                    'SampleFreCode': 4,
                    'SamplePointCategory': [
                        {
                            'Code': 0,
                            'Name': '256'
                        },
                        {
                            'Code': 1,
                            'Name': '512'
                        },
                        {
                            'Code': 2,
                            'Name': '1024'
                        },
                        {
                            'Code': 3,
                            'Name': '2048'
                        },
                        {
                            'Code': 4,
                            'Name': '4096'
                        },
                        {
                            'Code': 5,
                            'Name': '8192'
                        },
                        {
                            'Code': 6,
                            'Name': '16384'
                        }
                    ],
                    'SamplePointCode': 3,
                    'SlotNum': 1,
                    'Unit': 'mm/s',
                    'WirelessVibrationChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 3,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高正常',
                                            'Value': 3
                                        },
                                        {
                                            'Code': 5,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低正常',
                                            'Value': -3.5
                                        },
                                        {
                                            'Code': 6,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低预警告',
                                            'Value': -6.4000000953674316
                                        },
                                        {
                                            'Code': 7,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低警告',
                                            'Value': -10
                                        },
                                        {
                                            'Code': 8,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低危险',
                                            'Value': -10
                                        }
                                    ],
                                    'Mode': [
                                        {
                                            'Code': 0,
                                            'Name': '自动'
                                        },
                                        {
                                            'Code': 1,
                                            'Name': '单值'
                                        },
                                        {
                                            'Code': 2,
                                            'Name': '曲线或曲面'
                                        }
                                    ],
                                    'ModeCode': 0,
                                    'Para': [
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        },
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        }
                                    ]
                                }
                            },
                            'BiasVoltHigh': 5,
                            'BiasVoltLow': 0,
                            'CHNum': 23,
                            'DelayAlarmTime': 10000,
                            'DisplacementCalibration': 1,
                            'DivFreInfo': [
                                {
                                    'AlarmStrategy': {
                                        'Absolute': {
                                            'Category': [
                                                {
                                                    'Code': 0,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高危险',
                                                    'Value': 15
                                                },
                                                {
                                                    'Code': 1,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高警告',
                                                    'Value': 10
                                                },
                                                {
                                                    'Code': 2,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高预警告',
                                                    'Value': 6
                                                },
                                                {
                                                    'Code': 4,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '正常',
                                                    'Value': 0
                                                }
                                            ]
                                        },
                                        'Comparative': {
                                            'IntevalTime': 10000,
                                            'IsAllow': false,
                                            'Para': [
                                                {
                                                    'CHNum': 0,
                                                    'CardNum': 1
                                                }
                                            ],
                                            'Percent': 0.20000000298023224,
                                            'Range': ''
                                        }
                                    },
                                    'BasedOnRPM': {
                                        'Code': 0,
                                        'MultiFre': 1,
                                        'Name': '基于转速'
                                    },
                                    'BasedOnRange': {
                                        'Code': 2,
                                        'FreHigh': 0,
                                        'FreLow': 0,
                                        'MaxFreNum': 0,
                                        'Name': '基于范围'
                                    },
                                    'Code': '',
                                    'Create_Time': '',
                                    'DivFreCode': -1,
                                    'FixedFre': {
                                        'CharacteristicFre': 80,
                                        'Code': 1,
                                        'Name': '固定频率',
                                        'Percent': 0.05000000074505806
                                    },
                                    'Guid': '',
                                    'Modify_Time': '',
                                    'Name': 'example',
                                    'Remarks': '',
                                    'T_Item_Code': '',
                                    'T_Item_Guid': '',
                                    'T_Item_Name': ''
                                }
                            ],
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsMultiplication': false,
                            'IsSaveWaveToSD': true,
                            'IsUploadData': false,
                            'IsUploadWave': true,
                            'IsValidWave': true,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'MultiplicationCor': 1,
                            'Organization': [
                            ],
                            'PRMSlotNum': 0,
                            'RPMCHNum': 0,
                            'RPMCardNum': 1,
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 0,
                                    'Name': '有效值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 1,
                                    'Name': '峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 2,
                                    'Name': '峰峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 3,
                                    'Name': '平均值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 0,
                            'Sensitivity': 0.096000000834465027,
                            'SubCHNum': 0,
                            'TPDirCategory': [
                                {
                                    'Code': 0,
                                    'Degree': 90,
                                    'Name': 'Radial direction'
                                },
                                {
                                    'Code': 1,
                                    'Degree': 0,
                                    'Name': 'Axial direction'
                                }
                            ],
                            'TPDirCode': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号23',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点23',
                            'Unit': '',
                            'VelocityCalibration': 1,
                            'WaveUnit': ''
                        }
                    ]
                },
                'WorkTime': 2147483647
            },
            {
                'BatteryEnergy': 0.80000001192092896,
                'ExtraInfo': {
                },
                'Remarks': '',
                'SlaveIdentifier': 'FE010C05A8',
                'SleepTime': 0,
                'TransmissionName': '',
                'TransmissionType': 0,
                'Version': '',
                'WirelessScalarSlot': {
                    'SlotNum': 0,
                    'WirelessScalarChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 4,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '正常',
                                            'Value': 0
                                        }
                                    ]
                                }
                            },
                            'CHNum': 24,
                            'DelayAlarmTime': 10000,
                            'EquationCategory': [
                                {
                                    'CalibrationCor': 1,
                                    'Code': 0,
                                    'Formula': '',
                                    'Name': '一次线性方程'
                                },
                                {
                                    'CalibrationCor': 1,
                                    'Code': 1,
                                    'Formula': '',
                                    'Name': '一次非线性方程'
                                }
                            ],
                            'EquationCode': 0,
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsUploadData': false,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'Organization': [
                            ],
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 7,
                                    'Name': '数值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 7,
                            'SubCHNum': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号24',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点24',
                            'Unit': ''
                        }
                    ]
                },
                'WirelessVibrationSlot': {
                    'Integration': 1,
                    'SampleFreCategory': [
                        {
                            'Code': 7,
                            'Name': '338HZ'
                        },
                        {
                            'Code': 6,
                            'Name': '675HZ'
                        },
                        {
                            'Code': 5,
                            'Name': '1.35KHZ'
                        },
                        {
                            'Code': 4,
                            'Name': '2.7KHZ'
                        },
                        {
                            'Code': 3,
                            'Name': '5.4KHZ'
                        },
                        {
                            'Code': 2,
                            'Name': '10.8KHZ'
                        },
                        {
                            'Code': 1,
                            'Name': '21.6KHZ'
                        },
                        {
                            'Code': 0,
                            'Name': '43.2KHZ'
                        }
                    ],
                    'SampleFreCode': 4,
                    'SamplePointCategory': [
                        {
                            'Code': 0,
                            'Name': '256'
                        },
                        {
                            'Code': 1,
                            'Name': '512'
                        },
                        {
                            'Code': 2,
                            'Name': '1024'
                        },
                        {
                            'Code': 3,
                            'Name': '2048'
                        },
                        {
                            'Code': 4,
                            'Name': '4096'
                        },
                        {
                            'Code': 5,
                            'Name': '8192'
                        },
                        {
                            'Code': 6,
                            'Name': '16384'
                        }
                    ],
                    'SamplePointCode': 3,
                    'SlotNum': 1,
                    'Unit': 'mm/s',
                    'WirelessVibrationChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 3,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高正常',
                                            'Value': 3
                                        },
                                        {
                                            'Code': 5,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低正常',
                                            'Value': -3.5
                                        },
                                        {
                                            'Code': 6,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低预警告',
                                            'Value': -6.4000000953674316
                                        },
                                        {
                                            'Code': 7,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低警告',
                                            'Value': -10
                                        },
                                        {
                                            'Code': 8,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低危险',
                                            'Value': -10
                                        }
                                    ],
                                    'Mode': [
                                        {
                                            'Code': 0,
                                            'Name': '自动'
                                        },
                                        {
                                            'Code': 1,
                                            'Name': '单值'
                                        },
                                        {
                                            'Code': 2,
                                            'Name': '曲线或曲面'
                                        }
                                    ],
                                    'ModeCode': 0,
                                    'Para': [
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        },
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        }
                                    ]
                                }
                            },
                            'BiasVoltHigh': 5,
                            'BiasVoltLow': 0,
                            'CHNum': 25,
                            'DelayAlarmTime': 10000,
                            'DisplacementCalibration': 1,
                            'DivFreInfo': [
                                {
                                    'AlarmStrategy': {
                                        'Absolute': {
                                            'Category': [
                                                {
                                                    'Code': 0,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高危险',
                                                    'Value': 15
                                                },
                                                {
                                                    'Code': 1,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高警告',
                                                    'Value': 10
                                                },
                                                {
                                                    'Code': 2,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高预警告',
                                                    'Value': 6
                                                },
                                                {
                                                    'Code': 4,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '正常',
                                                    'Value': 0
                                                }
                                            ]
                                        },
                                        'Comparative': {
                                            'IntevalTime': 10000,
                                            'IsAllow': false,
                                            'Para': [
                                                {
                                                    'CHNum': 0,
                                                    'CardNum': 1
                                                }
                                            ],
                                            'Percent': 0.20000000298023224,
                                            'Range': ''
                                        }
                                    },
                                    'BasedOnRPM': {
                                        'Code': 0,
                                        'MultiFre': 1,
                                        'Name': '基于转速'
                                    },
                                    'BasedOnRange': {
                                        'Code': 2,
                                        'FreHigh': 0,
                                        'FreLow': 0,
                                        'MaxFreNum': 0,
                                        'Name': '基于范围'
                                    },
                                    'Code': '',
                                    'Create_Time': '',
                                    'DivFreCode': -1,
                                    'FixedFre': {
                                        'CharacteristicFre': 80,
                                        'Code': 1,
                                        'Name': '固定频率',
                                        'Percent': 0.05000000074505806
                                    },
                                    'Guid': '',
                                    'Modify_Time': '',
                                    'Name': 'example',
                                    'Remarks': '',
                                    'T_Item_Code': '',
                                    'T_Item_Guid': '',
                                    'T_Item_Name': ''
                                }
                            ],
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsMultiplication': false,
                            'IsSaveWaveToSD': true,
                            'IsUploadData': false,
                            'IsUploadWave': true,
                            'IsValidWave': true,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'MultiplicationCor': 1,
                            'Organization': [
                            ],
                            'PRMSlotNum': 0,
                            'RPMCHNum': 0,
                            'RPMCardNum': 1,
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 0,
                                    'Name': '有效值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 1,
                                    'Name': '峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 2,
                                    'Name': '峰峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 3,
                                    'Name': '平均值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 0,
                            'Sensitivity': 0.096000000834465027,
                            'SubCHNum': 0,
                            'TPDirCategory': [
                                {
                                    'Code': 0,
                                    'Degree': 90,
                                    'Name': 'Radial direction'
                                },
                                {
                                    'Code': 1,
                                    'Degree': 0,
                                    'Name': 'Axial direction'
                                }
                            ],
                            'TPDirCode': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号25',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点25',
                            'Unit': '',
                            'VelocityCalibration': 1,
                            'WaveUnit': ''
                        }
                    ]
                },
                'WorkTime': 2147483647
            },
            {
                'BatteryEnergy': 0.80000001192092896,
                'ExtraInfo': {
                },
                'Remarks': '',
                'SlaveIdentifier': 'FE010C05A9',
                'SleepTime': 0,
                'TransmissionName': '',
                'TransmissionType': 0,
                'Version': '',
                'WirelessScalarSlot': {
                    'SlotNum': 0,
                    'WirelessScalarChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 4,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '正常',
                                            'Value': 0
                                        }
                                    ]
                                }
                            },
                            'CHNum': 26,
                            'DelayAlarmTime': 10000,
                            'EquationCategory': [
                                {
                                    'CalibrationCor': 1,
                                    'Code': 0,
                                    'Formula': '',
                                    'Name': '一次线性方程'
                                },
                                {
                                    'CalibrationCor': 1,
                                    'Code': 1,
                                    'Formula': '',
                                    'Name': '一次非线性方程'
                                }
                            ],
                            'EquationCode': 0,
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsUploadData': false,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'Organization': [
                            ],
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 7,
                                    'Name': '数值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 7,
                            'SubCHNum': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号26',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点26',
                            'Unit': ''
                        }
                    ]
                },
                'WirelessVibrationSlot': {
                    'Integration': 1,
                    'SampleFreCategory': [
                        {
                            'Code': 7,
                            'Name': '338HZ'
                        },
                        {
                            'Code': 6,
                            'Name': '675HZ'
                        },
                        {
                            'Code': 5,
                            'Name': '1.35KHZ'
                        },
                        {
                            'Code': 4,
                            'Name': '2.7KHZ'
                        },
                        {
                            'Code': 3,
                            'Name': '5.4KHZ'
                        },
                        {
                            'Code': 2,
                            'Name': '10.8KHZ'
                        },
                        {
                            'Code': 1,
                            'Name': '21.6KHZ'
                        },
                        {
                            'Code': 0,
                            'Name': '43.2KHZ'
                        }
                    ],
                    'SampleFreCode': 4,
                    'SamplePointCategory': [
                        {
                            'Code': 0,
                            'Name': '256'
                        },
                        {
                            'Code': 1,
                            'Name': '512'
                        },
                        {
                            'Code': 2,
                            'Name': '1024'
                        },
                        {
                            'Code': 3,
                            'Name': '2048'
                        },
                        {
                            'Code': 4,
                            'Name': '4096'
                        },
                        {
                            'Code': 5,
                            'Name': '8192'
                        },
                        {
                            'Code': 6,
                            'Name': '16384'
                        }
                    ],
                    'SamplePointCode': 3,
                    'SlotNum': 1,
                    'Unit': 'mm/s',
                    'WirelessVibrationChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 3,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高正常',
                                            'Value': 3
                                        },
                                        {
                                            'Code': 5,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低正常',
                                            'Value': -3.5
                                        },
                                        {
                                            'Code': 6,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低预警告',
                                            'Value': -6.4000000953674316
                                        },
                                        {
                                            'Code': 7,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低警告',
                                            'Value': -10
                                        },
                                        {
                                            'Code': 8,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低危险',
                                            'Value': -10
                                        }
                                    ],
                                    'Mode': [
                                        {
                                            'Code': 0,
                                            'Name': '自动'
                                        },
                                        {
                                            'Code': 1,
                                            'Name': '单值'
                                        },
                                        {
                                            'Code': 2,
                                            'Name': '曲线或曲面'
                                        }
                                    ],
                                    'ModeCode': 0,
                                    'Para': [
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        },
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        }
                                    ]
                                }
                            },
                            'BiasVoltHigh': 5,
                            'BiasVoltLow': 0,
                            'CHNum': 27,
                            'DelayAlarmTime': 10000,
                            'DisplacementCalibration': 1,
                            'DivFreInfo': [
                                {
                                    'AlarmStrategy': {
                                        'Absolute': {
                                            'Category': [
                                                {
                                                    'Code': 0,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高危险',
                                                    'Value': 15
                                                },
                                                {
                                                    'Code': 1,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高警告',
                                                    'Value': 10
                                                },
                                                {
                                                    'Code': 2,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高预警告',
                                                    'Value': 6
                                                },
                                                {
                                                    'Code': 4,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '正常',
                                                    'Value': 0
                                                }
                                            ]
                                        },
                                        'Comparative': {
                                            'IntevalTime': 10000,
                                            'IsAllow': false,
                                            'Para': [
                                                {
                                                    'CHNum': 0,
                                                    'CardNum': 1
                                                }
                                            ],
                                            'Percent': 0.20000000298023224,
                                            'Range': ''
                                        }
                                    },
                                    'BasedOnRPM': {
                                        'Code': 0,
                                        'MultiFre': 1,
                                        'Name': '基于转速'
                                    },
                                    'BasedOnRange': {
                                        'Code': 2,
                                        'FreHigh': 0,
                                        'FreLow': 0,
                                        'MaxFreNum': 0,
                                        'Name': '基于范围'
                                    },
                                    'Code': '',
                                    'Create_Time': '',
                                    'DivFreCode': -1,
                                    'FixedFre': {
                                        'CharacteristicFre': 80,
                                        'Code': 1,
                                        'Name': '固定频率',
                                        'Percent': 0.05000000074505806
                                    },
                                    'Guid': '',
                                    'Modify_Time': '',
                                    'Name': 'example',
                                    'Remarks': '',
                                    'T_Item_Code': '',
                                    'T_Item_Guid': '',
                                    'T_Item_Name': ''
                                }
                            ],
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsMultiplication': false,
                            'IsSaveWaveToSD': true,
                            'IsUploadData': false,
                            'IsUploadWave': true,
                            'IsValidWave': true,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'MultiplicationCor': 1,
                            'Organization': [
                            ],
                            'PRMSlotNum': 0,
                            'RPMCHNum': 0,
                            'RPMCardNum': 1,
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 0,
                                    'Name': '有效值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 1,
                                    'Name': '峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 2,
                                    'Name': '峰峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 3,
                                    'Name': '平均值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 0,
                            'Sensitivity': 0.096000000834465027,
                            'SubCHNum': 0,
                            'TPDirCategory': [
                                {
                                    'Code': 0,
                                    'Degree': 90,
                                    'Name': 'Radial direction'
                                },
                                {
                                    'Code': 1,
                                    'Degree': 0,
                                    'Name': 'Axial direction'
                                }
                            ],
                            'TPDirCode': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号27',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点27',
                            'Unit': '',
                            'VelocityCalibration': 1,
                            'WaveUnit': ''
                        }
                    ]
                },
                'WorkTime': 2147483647
            },
            {
                'BatteryEnergy': 0.80000001192092896,
                'ExtraInfo': {
                },
                'Remarks': '',
                'SlaveIdentifier': 'FE010C05A10',
                'SleepTime': 0,
                'TransmissionName': '',
                'TransmissionType': 0,
                'Version': '',
                'WirelessScalarSlot': {
                    'SlotNum': 0,
                    'WirelessScalarChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 4,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '正常',
                                            'Value': 0
                                        }
                                    ]
                                }
                            },
                            'CHNum': 28,
                            'DelayAlarmTime': 10000,
                            'EquationCategory': [
                                {
                                    'CalibrationCor': 1,
                                    'Code': 0,
                                    'Formula': '',
                                    'Name': '一次线性方程'
                                },
                                {
                                    'CalibrationCor': 1,
                                    'Code': 1,
                                    'Formula': '',
                                    'Name': '一次非线性方程'
                                }
                            ],
                            'EquationCode': 0,
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsUploadData': false,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'Organization': [
                            ],
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 7,
                                    'Name': '数值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 7,
                            'SubCHNum': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号28',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点28',
                            'Unit': ''
                        }
                    ]
                },
                'WirelessVibrationSlot': {
                    'Integration': 1,
                    'SampleFreCategory': [
                        {
                            'Code': 7,
                            'Name': '338HZ'
                        },
                        {
                            'Code': 6,
                            'Name': '675HZ'
                        },
                        {
                            'Code': 5,
                            'Name': '1.35KHZ'
                        },
                        {
                            'Code': 4,
                            'Name': '2.7KHZ'
                        },
                        {
                            'Code': 3,
                            'Name': '5.4KHZ'
                        },
                        {
                            'Code': 2,
                            'Name': '10.8KHZ'
                        },
                        {
                            'Code': 1,
                            'Name': '21.6KHZ'
                        },
                        {
                            'Code': 0,
                            'Name': '43.2KHZ'
                        }
                    ],
                    'SampleFreCode': 4,
                    'SamplePointCategory': [
                        {
                            'Code': 0,
                            'Name': '256'
                        },
                        {
                            'Code': 1,
                            'Name': '512'
                        },
                        {
                            'Code': 2,
                            'Name': '1024'
                        },
                        {
                            'Code': 3,
                            'Name': '2048'
                        },
                        {
                            'Code': 4,
                            'Name': '4096'
                        },
                        {
                            'Code': 5,
                            'Name': '8192'
                        },
                        {
                            'Code': 6,
                            'Name': '16384'
                        }
                    ],
                    'SamplePointCode': 3,
                    'SlotNum': 1,
                    'Unit': 'mm/s',
                    'WirelessVibrationChannelInfo': [
                        {
                            'AlarmStrategy': {
                                'Absolute': {
                                    'Category': [
                                        {
                                            'Code': 0,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高危险',
                                            'Value': 15
                                        },
                                        {
                                            'Code': 1,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高警告',
                                            'Value': 10
                                        },
                                        {
                                            'Code': 2,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高预警告',
                                            'Value': 6
                                        },
                                        {
                                            'Code': 3,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '高正常',
                                            'Value': 3
                                        },
                                        {
                                            'Code': 5,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低正常',
                                            'Value': -3.5
                                        },
                                        {
                                            'Code': 6,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低预警告',
                                            'Value': -6.4000000953674316
                                        },
                                        {
                                            'Code': 7,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低警告',
                                            'Value': -10
                                        },
                                        {
                                            'Code': 8,
                                            'Formula': '',
                                            'IsAllow': true,
                                            'Name': '低危险',
                                            'Value': -10
                                        }
                                    ],
                                    'Mode': [
                                        {
                                            'Code': 0,
                                            'Name': '自动'
                                        },
                                        {
                                            'Code': 1,
                                            'Name': '单值'
                                        },
                                        {
                                            'Code': 2,
                                            'Name': '曲线或曲面'
                                        }
                                    ],
                                    'ModeCode': 0,
                                    'Para': [
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        },
                                        {
                                            'CHNum': 1,
                                            'CardNum': 2
                                        }
                                    ]
                                }
                            },
                            'BiasVoltHigh': 5,
                            'BiasVoltLow': 0,
                            'CHNum': 29,
                            'DelayAlarmTime': 10000,
                            'DisplacementCalibration': 1,
                            'DivFreInfo': [
                                {
                                    'AlarmStrategy': {
                                        'Absolute': {
                                            'Category': [
                                                {
                                                    'Code': 0,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高危险',
                                                    'Value': 15
                                                },
                                                {
                                                    'Code': 1,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高警告',
                                                    'Value': 10
                                                },
                                                {
                                                    'Code': 2,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '高预警告',
                                                    'Value': 6
                                                },
                                                {
                                                    'Code': 4,
                                                    'Formula': '',
                                                    'IsAllow': true,
                                                    'Name': '正常',
                                                    'Value': 0
                                                }
                                            ]
                                        },
                                        'Comparative': {
                                            'IntevalTime': 10000,
                                            'IsAllow': false,
                                            'Para': [
                                                {
                                                    'CHNum': 0,
                                                    'CardNum': 1
                                                }
                                            ],
                                            'Percent': 0.20000000298023224,
                                            'Range': ''
                                        }
                                    },
                                    'BasedOnRPM': {
                                        'Code': 0,
                                        'MultiFre': 1,
                                        'Name': '基于转速'
                                    },
                                    'BasedOnRange': {
                                        'Code': 2,
                                        'FreHigh': 0,
                                        'FreLow': 0,
                                        'MaxFreNum': 0,
                                        'Name': '基于范围'
                                    },
                                    'Code': '',
                                    'Create_Time': '',
                                    'DivFreCode': -1,
                                    'FixedFre': {
                                        'CharacteristicFre': 80,
                                        'Code': 1,
                                        'Name': '固定频率',
                                        'Percent': 0.05000000074505806
                                    },
                                    'Guid': '',
                                    'Modify_Time': '',
                                    'Name': 'example',
                                    'Remarks': '',
                                    'T_Item_Code': '',
                                    'T_Item_Guid': '',
                                    'T_Item_Name': ''
                                }
                            ],
                            'ExtraInfo': {
                            },
                            'IsBypass': false,
                            'IsLogic': false,
                            'IsMultiplication': false,
                            'IsSaveWaveToSD': true,
                            'IsUploadData': false,
                            'IsUploadWave': true,
                            'IsValidWave': true,
                            'LocalSaveCategory': [
                                {
                                    'Code': 0,
                                    'Name': '上传失败且报警数据'
                                },
                                {
                                    'Code': 1,
                                    'Name': '本地备份报警数据'
                                },
                                {
                                    'Code': 2,
                                    'Name': '上传失败就存储数据'
                                },
                                {
                                    'Code': 3,
                                    'Name': '本地备份所有数据'
                                },
                                {
                                    'Code': 4,
                                    'Name': '不存任何数据'
                                }
                            ],
                            'LocalSaveCode': 4,
                            'LogicExpression': '',
                            'MultiplicationCor': 1,
                            'Organization': [
                            ],
                            'PRMSlotNum': 0,
                            'RPMCHNum': 0,
                            'RPMCardNum': 1,
                            'Remarks': '',
                            'SVTypeCategory': [
                                {
                                    'Code': 0,
                                    'Name': '有效值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 1,
                                    'Name': '峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 2,
                                    'Name': '峰峰值',
                                    'ZeroValue': 0
                                },
                                {
                                    'Code': 3,
                                    'Name': '平均值',
                                    'ZeroValue': 0
                                }
                            ],
                            'SVTypeCode': 0,
                            'Sensitivity': 0.096000000834465027,
                            'SubCHNum': 0,
                            'TPDirCategory': [
                                {
                                    'Code': 0,
                                    'Degree': 90,
                                    'Name': 'Radial direction'
                                },
                                {
                                    'Code': 1,
                                    'Degree': 0,
                                    'Name': 'Axial direction'
                                }
                            ],
                            'TPDirCode': 0,
                            'T_Device_Code': '虚拟编号',
                            'T_Device_Guid': '',
                            'T_Device_Name': '虚拟设备',
                            'T_Item_Code': '编号29',
                            'T_Item_Guid': '',
                            'T_Item_Name': '测点29',
                            'Unit': '',
                            'VelocityCalibration': 1,
                            'WaveUnit': ''
                        }
                    ]
                },
                'WorkTime': 2147483647
            }
        ]
    }
}";
    }
}
