using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using ScintillaNET;
using W;

namespace ScriptTool
{
    public class Helpers
    {
        public static void InitHotkeys(System.Windows.Forms.Form form, ScintillaNET.Scintilla editor)
        {
            // register the hotkeys with the form
            //HotKeyManager.AddHotKey(form, OpenSearch, Keys.F, true);
            //HotKeyManager.AddHotKey(form, OpenFindDialog, Keys.F, true, false, true);
            //HotKeyManager.AddHotKey(form, OpenReplaceDialog, Keys.R, true);
            //HotKeyManager.AddHotKey(form, OpenReplaceDialog, Keys.H, true);
            //HotKeyManager.AddHotKey(form, Uppercase, Keys.U, true);
            //HotKeyManager.AddHotKey(form, Lowercase, Keys.L, true);
            HotKeyManager.AddHotKey(form, editor.ZoomIn, Keys.Oemplus, true);
            HotKeyManager.AddHotKey(form, editor.ZoomOut, Keys.OemMinus, true);
            HotKeyManager.AddHotKey(form, () => editor.Zoom = 0, Keys.D0, true);
            //HotKeyManager.AddHotKey(form, CloseSearch, Keys.Escape);

            // remove conflicting hotkeys from scintilla
            //editor.ClearCmdKey(Keys.Control | Keys.F);
            //editor.ClearCmdKey(Keys.Control | Keys.R);
            //editor.ClearCmdKey(Keys.Control | Keys.H);
            //editor.ClearCmdKey(Keys.Control | Keys.L);
            //editor.ClearCmdKey(Keys.Control | Keys.U);
        }

        public static void InitSyntaxColoring(ScintillaNET.Scintilla editor)
        {

            // Configure the default style
            editor.StyleResetDefault();
            editor.Styles[Style.Default].Font = "Consolas";
            editor.Styles[Style.Default].Size = 10;
            editor.Styles[Style.Default].BackColor = IntToColor(0x212121);
            editor.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
            editor.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            editor.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            editor.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            editor.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            editor.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            editor.Styles[Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            editor.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            editor.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            editor.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            editor.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            editor.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            editor.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            editor.Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            editor.Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            editor.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            editor.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            editor.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);

            editor.Lexer = Lexer.Cpp;

            editor.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            editor.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");
        }
        private static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }
    }
}
