using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DatabaseService.Tests
{
    static class JsonTest
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
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
                'Unit': '',
                'UploadIntevalTime': 10000
            },
            'AnalogRransducerOutSlot': {
                'AnalogRransducerOutChannelInfo': [
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                ],
                'InSignalCode': -2147483615,
                'IsInput': false,
                'SlotName': '模拟变送器输出',
                'SlotNum': 9,
                'Unit': '',
                'UploadIntevalTime': 10000
            },
            'CardName': 'card0',
            'CardNum': 0,
            'DigitRransducerInSlot': {
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                ],
                'InSignalCode': 35,
                'IsInput': true,
                'SlotName': '数字变送器输入',
                'SlotNum': 7,
                'Unit': '',
                'UploadIntevalTime': 10000
            },
            'DigitRransducerOutSlot': {
                'DigitRransducerOutChannelInfo': [
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                ],
                'InSignalCode': -2147483615,
                'IsInput': false,
                'SlotName': '数字变送器输出',
                'SlotNum': 8,
                'Unit': '',
                'UploadIntevalTime': 10000
            },
            'DigitTachometerSlot': {
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
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
                'Unit': 'RPM',
                'UploadIntevalTime': 10000
            },
            'EddyCurrentDisplacementSlot': {
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号0',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号1',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号2',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号3',
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
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
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
                'InSignalCategory': [
                    {
                        'Code': 0,
                        'Name': '电涡流传感器'
                    },
                    {
                        'Code': 1,
                        'Name': '缓存输入'
                    }
                ],
                'InSignalCode': 0,
                'Is24V': false,
                'IsInput': true,
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
                'SlotName': '电涡流位移',
                'SlotNum': 1,
                'Unit': 'um',
                'UploadIntevalTime': 10000,
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号0',
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
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 1,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号1',
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
                        'Unit': ''
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
                'InSignalCategory': [
                    {
                        'Code': 0,
                        'Name': '电涡流传感器'
                    },
                    {
                        'Code': 1,
                        'Name': '缓存输入'
                    }
                ],
                'InSignalCode': 0,
                'Is24V': false,
                'IsInput': true,
                'SlotName': '电涡流键相',
                'SlotNum': 2,
                'Unit': 'RPM',
                'UploadIntevalTime': 10000
            },
            'EddyCurrentTachometerSlot': {
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号0',
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
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 1,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号1',
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
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                    {
                        'Code': 0,
                        'Name': '电涡流传感器'
                    },
                    {
                        'Code': 1,
                        'Name': '缓存输入'
                    }
                ],
                'InSignalCode': 0,
                'Is24V': false,
                'IsEnableMainCH': true,
                'IsInput': true,
                'SlotName': '电涡流转速表',
                'SlotNum': 3,
                'Unit': 'RPM',
                'UploadIntevalTime': 10000
            },
            'IEPESlot': {
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号0',
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
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': '',
                        'VelocityCalibration': 1
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号1',
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
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': '',
                        'VelocityCalibration': 1
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号2',
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
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': '',
                        'VelocityCalibration': 1
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号3',
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
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': '',
                        'VelocityCalibration': 1
                    }
                ],
                'InSignalCategory': [
                    {
                        'Code': 0,
                        'Name': 'IEPE加速度传感器'
                    },
                    {
                        'Code': 1,
                        'Name': '缓存输入'
                    },
                    {
                        'Code': 2,
                        'Name': 'IEPE速度传感器'
                    },
                    {
                        'Code': 3,
                        'Name': 'IEPE位移传感器'
                    }
                ],
                'InSignalCode': 0,
                'Integration': 0,
                'IsInput': true,
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
                'SlotName': 'IEPE',
                'SlotNum': 0,
                'Unit': 'm/s^2',
                'UploadIntevalTime': 10000,
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': true,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
                'SlotName': '继电器',
                'SlotNum': 6,
                'Unit': '',
                'UploadIntevalTime': 10000
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
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
                'Unit': '',
                'UploadIntevalTime': 10000
            },
            'AnalogRransducerOutSlot': {
                'AnalogRransducerOutChannelInfo': [
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                ],
                'InSignalCode': 33,
                'IsInput': false,
                'SlotName': '模拟变送器输出',
                'SlotNum': 9,
                'Unit': '',
                'UploadIntevalTime': 10000
            },
            'CardName': 'card1',
            'CardNum': 1,
            'DigitRransducerInSlot': {
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Item_Code': '编号0',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                ],
                'InSignalCode': 35,
                'IsInput': true,
                'SlotName': '数字变送器输入',
                'SlotNum': 7,
                'Unit': '',
                'UploadIntevalTime': 10000
            },
            'DigitRransducerOutSlot': {
                'DigitRransducerOutChannelInfo': [
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                'Unit': '',
                'UploadIntevalTime': 10000
            },
            'DigitTachometerSlot': {
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
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
                'Unit': 'RPM',
                'UploadIntevalTime': 10000
            },
            'EddyCurrentDisplacementSlot': {
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号0',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号1',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号2',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号3',
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
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
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
                'InSignalCategory': [
                    {
                        'Code': 0,
                        'Name': '电涡流传感器'
                    },
                    {
                        'Code': 1,
                        'Name': '缓存输入'
                    }
                ],
                'InSignalCode': 0,
                'Is24V': false,
                'IsInput': true,
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
                'SlotName': '电涡流位移',
                'SlotNum': 1,
                'Unit': 'um',
                'UploadIntevalTime': 10000,
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号0',
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
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 1,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
                        'Remarks': '',
                        'SVTypeCategory': [
                            {
                                'Code': 7,
                                'Name': '数值',
                                'ZeroValue': 0
                            }
                        ],
                        'SVTypeCode': 7,
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号1',
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
                        'Unit': ''
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
                'InSignalCategory': [
                    {
                        'Code': 0,
                        'Name': '电涡流传感器'
                    },
                    {
                        'Code': 1,
                        'Name': '缓存输入'
                    }
                ],
                'InSignalCode': 0,
                'Is24V': false,
                'IsInput': true,
                'SlotName': '电涡流键相',
                'SlotNum': 2,
                'Unit': 'RPM',
                'UploadIntevalTime': 10000
            },
            'EddyCurrentTachometerSlot': {
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号0',
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
                        'BiasVoltHigh': -3,
                        'BiasVoltLow': -19,
                        'CHNum': 1,
                        'CalibrationCor': 1,
                        'DelayAlarmTime': 10000,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号1',
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
                        'Unit': ''
                    }
                ],
                'InSignalCategory': [
                    {
                        'Code': 0,
                        'Name': '电涡流传感器'
                    },
                    {
                        'Code': 1,
                        'Name': '缓存输入'
                    }
                ],
                'InSignalCode': 0,
                'Is24V': false,
                'IsEnableMainCH': true,
                'IsInput': true,
                'SlotName': '电涡流转速表',
                'SlotNum': 3,
                'Unit': 'RPM',
                'UploadIntevalTime': 10000
            },
            'IEPESlot': {
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号0',
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
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点0',
                        'Unit': '',
                        'VelocityCalibration': 1
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号1',
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
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点1',
                        'Unit': '',
                        'VelocityCalibration': 1
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号2',
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
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点2',
                        'Unit': '',
                        'VelocityCalibration': 1
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
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
                        'Extra_Information': '',
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
                        'Organization': '',
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
                        'Sensitivity': 0,
                        'SubCHNum': 0,
                        'T_Item_Code': '编号3',
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
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': '',
                        'VelocityCalibration': 1
                    }
                ],
                'InSignalCategory': [
                    {
                        'Code': 0,
                        'Name': 'IEPE加速度传感器'
                    },
                    {
                        'Code': 1,
                        'Name': '缓存输入'
                    },
                    {
                        'Code': 2,
                        'Name': 'IEPE速度传感器'
                    },
                    {
                        'Code': 3,
                        'Name': 'IEPE位移传感器'
                    }
                ],
                'InSignalCode': 0,
                'Integration': 0,
                'IsInput': true,
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
                'SlotName': 'IEPE',
                'SlotNum': 0,
                'Unit': 'm/s^2',
                'UploadIntevalTime': 10000,
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
                'InSignalCategory': [
                ],
                'InSignalCode': -2147483615,
                'IsInput': false,
                'RelayChannelInfo': [
                    {
                        'AlarmStrategy': {
                        },
                        'CHNum': 0,
                        'DelayAlarmTime': 10000,
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Device_Code': '虚拟编号',
                        'T_Device_Guid': '',
                        'T_Device_Name': '虚拟设备',
                        'Extra_Information': '',
                        'IsBypass': false,
                        'IsLogic': false,
                        'IsUploadData': false,
                        'LocalSaveCategory': [
                        ],
                        'LocalSaveCode': 0,
                        'LogicExpression': '',
                        'Organization': '',
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
                        'T_Item_Code': '编号3',
                        'T_Item_Guid': '',
                        'T_Item_Name': '测点3',
                        'Unit': ''
                    }
                ],
                'SlotName': '继电器',
                'SlotNum': 6,
                'Unit': '',
                'UploadIntevalTime': 10000
            }
        }
    ]
}
";
    }
}
