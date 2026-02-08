using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientCore;
using ClientGUI;
using DTAConfig.OptionPanels;
using Microsoft.Xna.Framework;
using Rampastring.XNAUI;
using Rampastring.XNAUI.XNAControls;

namespace DTAConfig.OptionPanels{
    public class PatchOptionsPanel: XNAOptionsPanel{
        private XNAClientCheckBox chk显示水印;
        private XNAClientCheckBox chk显示调试水印;

        public PatchOptionsPanel(WindowManager windowManager, UserINISettings iniSettings)
              : base(windowManager, iniSettings){
        }

        public override void Initialize()
        {
            base.Initialize();

            Name = "补丁设置";

            // 添加显示水印的复选框
            var lblDescription = new XNALabel(WindowManager);
            lblDescription.Name = "lblDescription";
            lblDescription.ClientRectangle = new Rectangle(10, 10, 0, 0);
            lblDescription.Text = "补丁设置选项";
            AddChild(lblDescription);

            chk显示水印 = new XNAClientCheckBox(WindowManager);
            chk显示水印.Name = nameof(chk显示水印);
            chk显示水印.ClientRectangle = new Rectangle(10, 40, 0, 0);
            chk显示水印.Text = "显示客户端水印";

            chk显示水印.AllowChecking = true;
            AddChild(chk显示水印);

            chk显示调试水印 = new XNAClientCheckBox(WindowManager);
            chk显示调试水印.Name = nameof(chk显示调试水印);
            chk显示调试水印.ClientRectangle = new Rectangle(10, 70, 0, 0);
            chk显示调试水印.Text = "显示调试水印";
            AddChild(chk显示调试水印);

        }

        public override void Load()
        {
            chk显示水印.Checked = UserINISettings.Instance.显示水印;
            chk显示调试水印.Checked = UserINISettings.Instance.显示调试水印;
            base.Load();
        }

        public override bool Save()
        {
            UserINISettings.Instance.显示水印.Value = chk显示水印.Checked;
            UserINISettings.Instance.显示调试水印.Value = chk显示调试水印.Checked;
            return base.Save();
        }
    }
}
