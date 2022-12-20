using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancePanelLibrary.Component.BaseElements;
using AdvancePanelLibrary.Component.BuildingBlocks.DesktopUI;
using AdvancePanelLibrary.Utility.Log;
//using AutoCreateWithJson.Component.BaseElements;
//using AutoCreateWithJson.Component.BuildingBlocks.DesktopUI;

namespace AutoCreateWithJson.Utility.Log
{
    internal static class OccureLog
    {


        public static void StartExecutorBuildingBlock(BasicBuildingBlock sender)
        {
           MyLog.ForceWritelnBoth($"StartExecutorBuildingBlock:{sender.GetType().Name}"); 
        }



        public static void ErrorToFindTargetElement(BasicBuildingBlock sender)
        {
           MyLog.WritelnBoth($"ErrorToFindTargetElement:{sender.GetType().Name}"); 
        }

        public static void ErrorToFindTargetElement(BasicBuildingBlock sender, Exception e)
        {
            MyLog.WritelnBoth($"ErrorToFindTargetElement:{sender.GetType().Name}",e.Message);
        }

        public static void ErrorInSetValue(BasicBuildingBlock sender)
        {
           MyLog.WritelnBoth($"ErrorToSetValue:{sender.GetType().Name}"); 
        }
          

        public static void ErrorInSetValue(BasicBuildingBlock sender, Exception e)
        {
            MyLog.WritelnBoth($"ErrorToSetValue:{sender.GetType().Name}",e.Message); 
        }


        public static void RunElementNotFound(BasicBuildingBlock sender)
        {
            MyLog.WritelnBoth($"RunElementNotFound:{sender.GetType().Name}");
        }

        public static void FinishWithErrorExecutorBuildingBlock(BasicBuildingBlock sender)
        {
            MyLog.WritelnBoth($"FinishWithError:{sender.GetType().Name}");
        }
        public static void FinishWithErrorExecutorBuildingBlock(BasicBuildingBlock sender,Exception e)
        {
            MyLog.WritelnBoth($"FinishWithError:{sender.GetType().Name}",e.Message);
        }

        public static void FinishExecutorBuildingBlock(BasicBuildingBlock sender)
        {
            MyLog.WritelnBoth($"FinishExecutorBuildingBlock:{sender.GetType().Name}");
        }

        public static void ErrorInClick(BasicBuildingBlock sender, Exception e)
        {
            MyLog.WritelnBoth($"ErrorInClick:{sender.GetType().Name}",e.Message);
        }

        public static void ErrorInClick(BasicBuildingBlock sender)
        {
            MyLog.WritelnBoth($"ErrorInClick:{sender.GetType().Name}");
        }

        public static void errorInFilterElement(BasicBuildingBlock sender, Exception e)
        {
            MyLog.WritelnBoth($"ErrorInFilter:{sender.GetType().Name}",e.Message);
        }
        public static void errorInFilterElement(BasicBuildingBlock sender)
        {
            MyLog.WritelnBoth($"ErrorInFilter:{sender.GetType().Name}");
        }

        public static void ErrorInSelect(BasicBuildingBlock sender, Exception e)
        {
            MyLog.WritelnBoth($"ErrorInClick:{sender.GetType().Name}");
            MyLog.WritelnFile($"ErrorInClick:{sender.GetType().Name}", e.Message);
        }
        public static void ErrorInSelect(BasicBuildingBlock sender)
        {
            MyLog.WritelnBoth($"ErrorInClick:{sender.GetType().Name}");
        }

        public static void ErrorToGetValue(BldBlkGetUIText bldBlkGetUiText, Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
