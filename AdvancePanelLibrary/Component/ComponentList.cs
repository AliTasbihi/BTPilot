using AdvancePanelLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shell;

namespace AdvancePanelLibrary.Component
{
    public static class ComponentList
    {
        static readonly List<BuildingBlockList> buildingBlockList = new();
        static ComponentList()
        {
            BuildingBlockList oneBlock;

            oneBlock = new BuildingBlockList();
            oneBlock.BuildingBlockName = "DataDriven";
            oneBlock.BuildingBlockComponnent.Add("BldBlkCommandLine");
            oneBlock.BuildingBlockComponnent.Add("BldBlkDatabase");
            oneBlock.BuildingBlockComponnent.Add("BldBlkHTTPRequest");
            oneBlock.BuildingBlockComponnent.Add("BldBlkReadExcel");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSendEMail");
            oneBlock.BuildingBlockComponnent.Add("BldBlkWriteExcel");
            buildingBlockList.Add(oneBlock);

            oneBlock = new BuildingBlockList();
            oneBlock.BuildingBlockName = "DesktopUI";
            oneBlock.BuildingBlockComponnent.Add("BldBlkClickUIElement");
            oneBlock.BuildingBlockComponnent.Add("BldBlkCloseUIWindow");
            oneBlock.BuildingBlockComponnent.Add("BldBlkCollapseUIElement");
            oneBlock.BuildingBlockComponnent.Add("BldBlkDragUIElement");
            oneBlock.BuildingBlockComponnent.Add("BldBlkExpandUIElement");
            oneBlock.BuildingBlockComponnent.Add("BldBlkFindUIElement");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetUINumber");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetUISelection");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetUIText");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetWindowDetails");
            oneBlock.BuildingBlockComponnent.Add("BldBlkInvokeUIElement");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSelectUIElement");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSetUIElementValue");
            oneBlock.BuildingBlockComponnent.Add("BldBlkStartApplication");
            oneBlock.BuildingBlockComponnent.Add("BldBlkToggleUIElement");
            oneBlock.BuildingBlockComponnent.Add("BldBlkUpdateUIWindow");
            oneBlock.BuildingBlockComponnent.Add("BldBlkUseUIWindow");
            buildingBlockList.Add(oneBlock);

            oneBlock = new BuildingBlockList();
            oneBlock.BuildingBlockName = "FindAndGet";
            oneBlock.BuildingBlockComponnent.Add("BldBlkFindImage");
            oneBlock.BuildingBlockComponnent.Add("BldBlkFindText");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetNumber");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetText");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSelectText");
            buildingBlockList.Add(oneBlock);

            oneBlock = new BuildingBlockList();
            oneBlock.BuildingBlockName = "Generators";
            oneBlock.BuildingBlockComponnent.Add("GenerateDateTime");
            oneBlock.BuildingBlockComponnent.Add("GenerateNumber");
            oneBlock.BuildingBlockComponnent.Add("GeneratePassword");
            buildingBlockList.Add(oneBlock);

            oneBlock = new BuildingBlockList();
            oneBlock.BuildingBlockName = "InputAndOutput";
            oneBlock.BuildingBlockComponnent.Add("BldBlkExecutionInput");
            oneBlock.BuildingBlockComponnent.Add("BldBlkExecutionOutput");
            oneBlock.BuildingBlockComponnent.Add("BldBlkValueInput");
            oneBlock.BuildingBlockComponnent.Add("BldBlkValueOutput");
            buildingBlockList.Add(oneBlock);

            oneBlock = new BuildingBlockList();
            oneBlock.BuildingBlockName = "Logic";
            oneBlock.BuildingBlockComponnent.Add("BldBlkBreakIteration");
            oneBlock.BuildingBlockComponnent.Add("BldBlkCalculate");
            oneBlock.BuildingBlockComponnent.Add("BldBlkChangeText");
            oneBlock.BuildingBlockComponnent.Add("BldBlkCloseWindows");
            oneBlock.BuildingBlockComponnent.Add("BldBlkCompare");
            oneBlock.BuildingBlockComponnent.Add("BldBlkCSharpCode");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetClipboard");
            oneBlock.BuildingBlockComponnent.Add("BldBlkLoop");
            oneBlock.BuildingBlockComponnent.Add("BldBlkOffsetArea");
            oneBlock.BuildingBlockComponnent.Add("BldBlkOffsetDateTime");
            oneBlock.BuildingBlockComponnent.Add("BldBlkOffsetPosition");
            oneBlock.BuildingBlockComponnent.Add("BldBlkRegularExpression");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSetClipboard");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSwitch");
            oneBlock.BuildingBlockComponnent.Add("BldBlkWait");
            buildingBlockList.Add(oneBlock);

            oneBlock = new BuildingBlockList();
            oneBlock.BuildingBlockName = "MouseAndKeyboard";
            oneBlock.BuildingBlockComponnent.Add("BldBlkClickImage");
            oneBlock.BuildingBlockComponnent.Add("BldBlkClickPosition");
            oneBlock.BuildingBlockComponnent.Add("BldBlkClickText");
            oneBlock.BuildingBlockComponnent.Add("BldBlkDragMouse");
            oneBlock.BuildingBlockComponnent.Add("BldBlkHoverImage");
            oneBlock.BuildingBlockComponnent.Add("BldBlkHoverPosition");
            oneBlock.BuildingBlockComponnent.Add("BldBlkHoverText");
            oneBlock.BuildingBlockComponnent.Add("BldBlkPressKey");
            oneBlock.BuildingBlockComponnent.Add("BldBlkReleaseKey");
            oneBlock.BuildingBlockComponnent.Add("BldBlkScrollWheel");
            oneBlock.BuildingBlockComponnent.Add("BldBlkTypeText");
            buildingBlockList.Add(oneBlock);

            oneBlock = new BuildingBlockList();
            oneBlock.BuildingBlockName = "SettingValues";
            oneBlock.BuildingBlockComponnent.Add("BldBlkSetArea");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSetDateTime");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSetNumber");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSetPosition");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSetText");
            buildingBlockList.Add(oneBlock);

            oneBlock = new BuildingBlockList();
            oneBlock.BuildingBlockName = "StartAndStop";
            oneBlock.BuildingBlockComponnent.Add("BldBlkDone");
            oneBlock.BuildingBlockComponnent.Add("BldBlkFail");
            oneBlock.BuildingBlockComponnent.Add("BldBlkPass");
            oneBlock.BuildingBlockComponnent.Add("BldBlkStart");
            buildingBlockList.Add(oneBlock);

            oneBlock = new BuildingBlockList();
            oneBlock.BuildingBlockName = "System";
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetAgentSession");
            oneBlock.BuildingBlockComponnent.Add("BldBlkLock");
            oneBlock.BuildingBlockComponnent.Add("BldBlkLogin");
            oneBlock.BuildingBlockComponnent.Add("BldBlkLogout");
            buildingBlockList.Add(oneBlock);

            oneBlock = new BuildingBlockList();
            oneBlock.BuildingBlockName = "Variables";
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetVariable");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSetVariable");
            buildingBlockList.Add(oneBlock);

            oneBlock = new BuildingBlockList();
            oneBlock.BuildingBlockName = "Web";
            oneBlock.BuildingBlockComponnent.Add("BldBlkClearWebCookie");
            oneBlock.BuildingBlockComponnent.Add("BldBlkClickWebElement");
            oneBlock.BuildingBlockComponnent.Add("BldBlkClickWebPosition");
            oneBlock.BuildingBlockComponnent.Add("BldBlkCloseWebBrowser");
            oneBlock.BuildingBlockComponnent.Add("BldBlkDragWebElement");
            oneBlock.BuildingBlockComponnent.Add("BldBlkFindWebElement");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetBrowserDetails");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetWebAttribute");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetWebCheckbox");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetWebCookie");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetWebDropdown");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetWebNumber");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetWebRadioButton");
            oneBlock.BuildingBlockComponnent.Add("BldBlkGetWebText");
            oneBlock.BuildingBlockComponnent.Add("BldBlkHandleWebAlert");
            oneBlock.BuildingBlockComponnent.Add("BldBlkHoverWebElement");
            oneBlock.BuildingBlockComponnent.Add("BldBlkHoverWebPosition");
            oneBlock.BuildingBlockComponnent.Add("BldBlkLogWebScreenshot");
            oneBlock.BuildingBlockComponnent.Add("BldBlkNavigateWeb");
            oneBlock.BuildingBlockComponnent.Add("BldBlkRunWebJavascript");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSetWebCheckbox");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSetWebCookie");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSetWebDropdown");
            oneBlock.BuildingBlockComponnent.Add("BldBlkSetWebRadioButton");
            oneBlock.BuildingBlockComponnent.Add("BldBlkStartWebBrowser");
            oneBlock.BuildingBlockComponnent.Add("BldBlkTypeWebText");
            oneBlock.BuildingBlockComponnent.Add("BldBlkUseBrowserWindow");
            oneBlock.BuildingBlockComponnent.Add("BldBlkWebFileUpload");
            buildingBlockList.Add(oneBlock);


        }

        public static void FillComboBoxBuildingBlock(ComboBox.ObjectCollection items)
        {
            foreach(var bld in buildingBlockList)
            {
                var cm = GlobalFunction.BuildingBlockCategoryDecompress(bld.BuildingBlockName);
                items.Add(cm);
            }
        }

       public static void FillComboBoxBuildingBlockComponent(string _buildingBlockName, ComboBox.ObjectCollection items)
        {
            var buildingBlockName= GlobalFunction.BuildingBlockCategoryCompress(_buildingBlockName);
            foreach (var bld in buildingBlockList)
            {
               if (string.Equals(bld.BuildingBlockName, buildingBlockName, StringComparison.InvariantCulture))
                {
                    foreach(var compName in bld.BuildingBlockComponnent)
                    {
                        var cm=GlobalFunction.BuildingBlockComponentDecompress(compName);
                        items.Add(cm);
                    }
                }
            }
        }

        //Sample use
        //ComponentList.GetListOfAllComponentsByPath(@"D:\VS Projects\AutoCreateWithJson\AdvancePanelLibrary\Component\BuildingBlocks\");
        public static string GetListOfAllComponentsByPath(string path)
        {
            var files = Directory.EnumerateDirectories(path, "*.*", SearchOption.TopDirectoryOnly);
            var sb = new StringBuilder();
            sb.AppendLine("BuildingBlockList oneBlock; ");
            foreach (var file in files)
            {
                var folderName = Path.GetFileName(file);
                sb.AppendLine("oneBlock= new BuildingBlockList(); ");
                sb.AppendLine($"oneBlock.BuildingBlockName = \"{folderName}\"; ");

                var filesSub = Directory.EnumerateFiles(path + folderName, "*.cs", SearchOption.TopDirectoryOnly);
                foreach (var fsb in filesSub)
                {
                    var fileName = Path.GetFileNameWithoutExtension(fsb);
                    sb.AppendLine($"oneBlock.BuildingBlockComponnent.Add(\"{fileName}\"); ");
                    //sb.AppendLine("  " + fileName);
                }
                sb.AppendLine("buildingBlockList.Add(oneBlock); ");
                sb.AppendLine("");
            }

            return sb.ToString();
        }
    }

    public class BuildingBlockList
    {
        public string BuildingBlockName;
        public List<string> BuildingBlockComponnent = new();
    }
}
