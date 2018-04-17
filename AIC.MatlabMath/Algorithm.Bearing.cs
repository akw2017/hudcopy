using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace AIC.MatlabMath
{
    public partial class Algorithm
    {
        // 轴承内环特征频率
        // PitchDiameter 节圆直径。
        // NumberOfRoller 滚子个数。
        // RollerDiameter 滚子直径。
        // ContactAngle 接触角。
        // 返回参数：小于0，参数设置有误，大于为0内环特征频率。
        delegate double GetBearingInnerRingFrequency(double PitchDiameter, int NumberOfRoller, double RollerDiameter, double ContactAngle);

        // 轴承外环特征频率
        // PitchDiameter 节圆直径。
        // NumberOfRoller 滚子个数。
        // RollerDiameter 滚子直径。
        // ContactAngle 接触角。
        // 返回参数：小于0，参数设置有误，大于0为外环特征频率。
        delegate double GetBearingOuterRingFrequency(double PitchDiameter, int NumberOfRoller, double RollerDiameter, double ContactAngle);


        //轴承滚动体特征频率
        // PitchDiameter 节圆直径。
        // NumberOfRoller 滚子个数。
        // RollerDiameter 滚子直径。
        // ContactAngle 接触角。
        // 返回参数：小于0，参数设置有误，大于0为滚动体特征频率。
        delegate double GetBearingRollerFrequency(double PitchDiameter, int NumberOfRoller, double RollerDiameter, double ContactAngle);

        //轴承保持架特征频率
        // PitchDiameter 节圆直径。
        // NumberOfRoller 滚子个数。
        // RollerDiameter 滚子直径。
        // ContactAngle 接触角。
        // 返回参数：小于0，参数设置有误，大于0为保持架特征频率。
        delegate double GetBearingMaintainsFrequency(double PitchDiameter, int NumberOfRoller, double RollerDiameter, double ContactAngle);


        private GetBearingInnerRingFrequency getBearingInnerRingFrequency;
        private GetBearingOuterRingFrequency getBearingOuterRingFrequency;
        private GetBearingRollerFrequency getBearingRollerFrequency;
        private GetBearingMaintainsFrequency getBearingMaintainsFrequency;

        private void InitializeBearing(int hModule)
        {
            IntPtr intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "GetBearingInnerRingFrequency");
            getBearingInnerRingFrequency = (GetBearingInnerRingFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(GetBearingInnerRingFrequency));

            intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "GetBearingOuterRingFrequency");
            getBearingOuterRingFrequency = (GetBearingOuterRingFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(GetBearingOuterRingFrequency));

            intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "GetBearingRollerFrequency");
            getBearingRollerFrequency = (GetBearingRollerFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(GetBearingRollerFrequency));

            intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "GetBearingMaintainsFrequency");
            getBearingMaintainsFrequency = (GetBearingMaintainsFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(GetBearingMaintainsFrequency));
        }

        public double GetBearingInnerRingFrequencyAction(double PitchDiameter, int NumberOfRoller, double RollerDiameter, double ContactAnglen)
        {
            return getBearingInnerRingFrequency(PitchDiameter, NumberOfRoller, RollerDiameter, ContactAnglen);
        }

        public double GetBearingOuterRingFrequencyAction(double PitchDiameter, int NumberOfRoller, double RollerDiameter, double ContactAnglen)
        {
            return getBearingOuterRingFrequency(PitchDiameter, NumberOfRoller, RollerDiameter, ContactAnglen);
        }

        public double GetBearingRollerFrequencyAction(double PitchDiameter, int NumberOfRoller, double RollerDiameter, double ContactAnglen)
        {
            return getBearingRollerFrequency(PitchDiameter, NumberOfRoller, RollerDiameter, ContactAnglen);
        }

        public double GetBearingMaintainsFrequencyAction(double PitchDiameter, int NumberOfRoller, double RollerDiameter, double ContactAnglen)
        {
            return getBearingMaintainsFrequency(PitchDiameter, NumberOfRoller, RollerDiameter, ContactAnglen);
        }
    }
}
