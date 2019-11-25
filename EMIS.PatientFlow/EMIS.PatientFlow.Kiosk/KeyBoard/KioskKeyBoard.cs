using System.Windows;
using System.Windows.Input;
using EMIS.PatientFlow.Kiosk.Helper;
namespace EMIS.PatientFlow.Kiosk.KeyBoard
{
	public class KioskKeyBoard : Window
	{
		
		public static Window InstanceObject { get; set; }

		#region Width and Height Keyboard
		protected static double _widthTouchKeyboard { get; set; }
		protected static double _heightTouchKeyboard { get; set; }
		#endregion

		#region Shift CapsLock Key
		protected static bool ShiftFlag { get; set; }
		protected static bool CapsLockFlag { get; set; }
		#endregion

		#region current controlling styling
		protected static System.Windows.Controls.Control _CurrentControl { get; set; }
		protected static System.Windows.Media.Brush _PreviousTextBoxBackgroundBrush { get; set; }
		protected static System.Windows.Media.Brush _PreviousTextBoxBorderBrush { get; set; }
		protected static Thickness _PreviousTextBoxBorderThickness { get; set; }
		#endregion

		#region Touch screen Text string
		public static string TouchScreenText
		{
			get
			{
				if (_CurrentControl is System.Windows.Controls.TextBox)
				{
					return ((System.Windows.Controls.TextBox)_CurrentControl).Text;
				}
				else if (_CurrentControl is System.Windows.Controls.PasswordBox)
				{
					return ((System.Windows.Controls.PasswordBox)_CurrentControl).Password;
				}
				else return string.Empty;
			}
			set
			{
				if (_CurrentControl is System.Windows.Controls.TextBox)
				{
					((System.Windows.Controls.TextBox)_CurrentControl).Text = value;
				}
				else if (_CurrentControl is System.Windows.Controls.PasswordBox)
				{
					((System.Windows.Controls.PasswordBox)_CurrentControl).Password = value;
				}
			}
		}
		#endregion

		#region Commands
		public static RoutedUICommand CmdTlide { get; set; }
		public static RoutedUICommand Cmd1 { get; set; }
		public static RoutedUICommand Cmd2 { get; set; }
		public static RoutedUICommand Cmd3 { get; set; }
		public static RoutedUICommand Cmd4 { get; set; }
		public static RoutedUICommand Cmd5 { get; set; }
		public static RoutedUICommand Cmd6 { get; set; }
		public static RoutedUICommand Cmd7 { get; set; }
		public static RoutedUICommand Cmd8 { get; set; }
		public static RoutedUICommand Cmd9 { get; set; }
		public static RoutedUICommand Cmd0 { get; set; }
		public static RoutedUICommand CmdPlus { get; set; }
		public static RoutedUICommand CmdMinus { get; set; }
		public static RoutedUICommand CmdBackspace { get; set; }
		public static RoutedUICommand CmdTab { get; set; }
		public static RoutedUICommand Cmdw { get; set; }
		public static RoutedUICommand CmdE { get; set; }
		public static RoutedUICommand CmdQ { get; set; }
		public static RoutedUICommand CmdR { get; set; }
		public static RoutedUICommand CmdT { get; set; }
		public static RoutedUICommand CmdY { get; set; }
		public static RoutedUICommand CmdU { get; set; }
		public static RoutedUICommand CmdI { get; set; }
		public static RoutedUICommand CmdO { get; set; }
		public static RoutedUICommand CmdP { get; set; }
		public static RoutedUICommand CmdOpenCrulyBrace { get; set; }
		public static RoutedUICommand CmdEndCrultBrace { get; set; }
		public static RoutedUICommand CmdOR { get; set; }
		public static RoutedUICommand CmdCapsLock { get; set; }
		public static RoutedUICommand CmdS { get; set; }
		public static RoutedUICommand CmdD { get; set; }
		public static RoutedUICommand CmdA { get; set; }
		public static RoutedUICommand CmdF { get; set; }
		public static RoutedUICommand CmdG { get; set; }
		public static RoutedUICommand CmdH { get; set; }
		public static RoutedUICommand CmdJ { get; set; }
		public static RoutedUICommand CmdK { get; set; }
		public static RoutedUICommand CmdL { get; set; }
		public static RoutedUICommand CmdColon { get; set; }
		public static RoutedUICommand CmdDoubleInvertedComma { get; set; }
		public static RoutedUICommand CmdEnter { get; set; }
		public static RoutedUICommand CmdShift { get; set; }
		public static RoutedUICommand CmdZ { get; set; }
		public static RoutedUICommand CmdX { get; set; }
		public static RoutedUICommand CmdC { get; set; }
		public static RoutedUICommand CmdV { get; set; }
		public static RoutedUICommand CmdB { get; set; }
		public static RoutedUICommand CmdN { get; set; }
		public static RoutedUICommand CmdM { get; set; }
		public static RoutedUICommand CmdGreaterThan { get; set; }
		public static RoutedUICommand CmdLessThan { get; set; }
		public static RoutedUICommand CmdQuestion { get; set; }
		public static RoutedUICommand CmdSpaceBar { get; set; }
		public static RoutedUICommand CmdClear { get; set; }
		public static RoutedUICommand CmdAtTheRate { get; set; }
		public static RoutedUICommand CmdHiphen { get; set; }

		#endregion

		public KioskKeyBoard()
		{
			Width = _widthTouchKeyboard;
			Height = _heightTouchKeyboard;
		}

		static KioskKeyBoard()
		{
			_widthTouchKeyboard = SystemParameters.PrimaryScreenWidth;
			_heightTouchKeyboard = (int)(SystemParameters.PrimaryScreenHeight) / 5 + 100;
			CmdTlide = new RoutedUICommand();
			Cmd1 = new RoutedUICommand();
			Cmd2 = new RoutedUICommand();
			Cmd3 = new RoutedUICommand();
			Cmd4 = new RoutedUICommand();
			Cmd5 = new RoutedUICommand();
			Cmd6 = new RoutedUICommand();
			Cmd7 = new RoutedUICommand();
			Cmd8 = new RoutedUICommand();
			Cmd9 = new RoutedUICommand();
			Cmd0 = new RoutedUICommand();
			CmdPlus = new RoutedUICommand();
			CmdMinus = new RoutedUICommand();
			CmdTab = new RoutedUICommand();
			CmdBackspace = new RoutedUICommand();
			Cmdw = new RoutedUICommand();
			CmdE = new RoutedUICommand();
			CmdQ = new RoutedUICommand();
			CmdR = new RoutedUICommand();
			CmdT = new RoutedUICommand();
			CmdY = new RoutedUICommand();
			CmdU = new RoutedUICommand();
			CmdI = new RoutedUICommand();
			CmdO = new RoutedUICommand();
			CmdP = new RoutedUICommand();
			CmdOpenCrulyBrace = new RoutedUICommand();
			CmdEndCrultBrace = new RoutedUICommand();
			CmdOR = new RoutedUICommand();
			CmdCapsLock = new RoutedUICommand();
			CmdS = new RoutedUICommand();
			CmdD = new RoutedUICommand();
			CmdA = new RoutedUICommand();
			CmdF = new RoutedUICommand();
			CmdG = new RoutedUICommand();
			CmdH = new RoutedUICommand();
			CmdJ = new RoutedUICommand();
			CmdK = new RoutedUICommand();
			CmdL = new RoutedUICommand();
			CmdColon = new RoutedUICommand();
			CmdDoubleInvertedComma = new RoutedUICommand();
			CmdEnter = new RoutedUICommand();
			CmdShift = new RoutedUICommand();
			CmdZ = new RoutedUICommand();
			CmdX = new RoutedUICommand();
			CmdC = new RoutedUICommand();
			CmdV = new RoutedUICommand();
			CmdB = new RoutedUICommand();
			CmdN = new RoutedUICommand();
			CmdM = new RoutedUICommand();
			CmdGreaterThan = new RoutedUICommand();
			CmdLessThan = new RoutedUICommand();
			CmdQuestion = new RoutedUICommand();
			CmdSpaceBar = new RoutedUICommand();
			CmdClear = new RoutedUICommand();
			CmdAtTheRate = new RoutedUICommand();
			CmdHiphen = new RoutedUICommand();
		}


		#region CommandBinding
		public static void SetCommandBinding()
		{
			CommandBinding CbTlide = new CommandBinding(CmdTlide, RunCommand);
			CommandBinding Cb2 = new CommandBinding(Cmd2, RunCommand);
			CommandBinding Cb3 = new CommandBinding(Cmd3, RunCommand);
			CommandBinding Cb4 = new CommandBinding(Cmd4, RunCommand);
			CommandBinding Cb1 = new CommandBinding(Cmd1, RunCommand);
			CommandBinding Cb5 = new CommandBinding(Cmd5, RunCommand);
			CommandBinding Cb6 = new CommandBinding(Cmd6, RunCommand);
			CommandBinding Cb7 = new CommandBinding(Cmd7, RunCommand);
			CommandBinding Cb8 = new CommandBinding(Cmd8, RunCommand);
			CommandBinding Cb9 = new CommandBinding(Cmd9, RunCommand);
			CommandBinding Cb0 = new CommandBinding(Cmd0, RunCommand);
			CommandBinding CbMinus = new CommandBinding(CmdMinus, RunCommand);
			CommandBinding CbPlus = new CommandBinding(CmdPlus, RunCommand);
			CommandBinding CbBackspace = new CommandBinding(CmdBackspace, RunCommand);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbTlide);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), Cb1);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), Cb2);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), Cb3);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), Cb4);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), Cb5);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), Cb6);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), Cb7);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), Cb8);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), Cb9);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), Cb0);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbMinus);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbPlus);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbBackspace);

			CommandBinding CbHiphen = new CommandBinding(CmdHiphen, RunCommand);
			CommandBinding CbAtTheRate = new CommandBinding(CmdAtTheRate, RunCommand);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbAtTheRate);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbHiphen);


			CommandBinding CbTab = new CommandBinding(CmdTab, RunCommand);
			CommandBinding CbQ = new CommandBinding(CmdQ, RunCommand);
			CommandBinding Cbw = new CommandBinding(Cmdw, RunCommand);
			CommandBinding CbE = new CommandBinding(CmdE, RunCommand);
			CommandBinding CbR = new CommandBinding(CmdR, RunCommand);
			CommandBinding CbT = new CommandBinding(CmdT, RunCommand);
			CommandBinding CbY = new CommandBinding(CmdY, RunCommand);
			CommandBinding CbU = new CommandBinding(CmdU, RunCommand);
			CommandBinding CbI = new CommandBinding(CmdI, RunCommand);
			CommandBinding Cbo = new CommandBinding(CmdO, RunCommand);
			CommandBinding CbP = new CommandBinding(CmdP, RunCommand);
			CommandBinding CbOpenCrulyBrace = new CommandBinding(CmdOpenCrulyBrace, RunCommand);
			CommandBinding CbEndCrultBrace = new CommandBinding(CmdEndCrultBrace, RunCommand);
			CommandBinding CbOR = new CommandBinding(CmdOR, RunCommand);

			CommandBinding CbCapsLock = new CommandBinding(CmdCapsLock, RunCommand);
			CommandBinding CbA = new CommandBinding(CmdA, RunCommand);
			CommandBinding CbS = new CommandBinding(CmdS, RunCommand);
			CommandBinding CbD = new CommandBinding(CmdD, RunCommand);
			CommandBinding CbF = new CommandBinding(CmdF, RunCommand);
			CommandBinding CbG = new CommandBinding(CmdG, RunCommand);
			CommandBinding CbH = new CommandBinding(CmdH, RunCommand);
			CommandBinding CbJ = new CommandBinding(CmdJ, RunCommand);
			CommandBinding CbK = new CommandBinding(CmdK, RunCommand);
			CommandBinding CbL = new CommandBinding(CmdL, RunCommand);
			CommandBinding CbColon = new CommandBinding(CmdColon, RunCommand);
			CommandBinding CbDoubleInvertedComma = new CommandBinding(CmdDoubleInvertedComma, RunCommand);
			CommandBinding CbEnter = new CommandBinding(CmdEnter, RunCommand);

			CommandBinding CbShift = new CommandBinding(CmdShift, RunCommand);
			CommandBinding CbZ = new CommandBinding(CmdZ, RunCommand);
			CommandBinding CbX = new CommandBinding(CmdX, RunCommand);
			CommandBinding CbC = new CommandBinding(CmdC, RunCommand);
			CommandBinding CbV = new CommandBinding(CmdV, RunCommand);
			CommandBinding CbB = new CommandBinding(CmdB, RunCommand);
			CommandBinding CbN = new CommandBinding(CmdN, RunCommand);
			CommandBinding CbM = new CommandBinding(CmdM, RunCommand);
			CommandBinding CbGreaterThan = new CommandBinding(CmdGreaterThan, RunCommand);
			CommandBinding CbLessThan = new CommandBinding(CmdLessThan, RunCommand);
			CommandBinding CbQuestion = new CommandBinding(CmdQuestion, RunCommand);

			CommandBinding CbSpaceBar = new CommandBinding(CmdSpaceBar, RunCommand);
			CommandBinding CbClear = new CommandBinding(CmdClear, RunCommand);

			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbTab);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbQ);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), Cbw);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbE);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbR);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbT);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbY);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbU);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbI);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), Cbo);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbP);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbOpenCrulyBrace);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbEndCrultBrace);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbOR);

			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbCapsLock);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbA);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbS);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbD);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbF);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbG);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbH);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbJ);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbK);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbL);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbColon);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbDoubleInvertedComma);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbEnter);

			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbShift);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbZ);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbX);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbC);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbV);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbB);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbN);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbM);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbGreaterThan);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbLessThan);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbQuestion);

			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbSpaceBar);
			CommandManager.RegisterClassCommandBinding(typeof(KioskKeyBoard), CbClear);
		}
		#endregion

		#region Keyboard Input
		private static void RunCommand(object sender, ExecutedRoutedEventArgs e)
		{
			if (TouchScreenText.Length <= GlobalVariables.AnswerOptionLimit - 1)
			{
				if (e.Command == CmdTlide)
				{
					TouchScreenText += (!ShiftFlag) ? "`" : ShiftMethod("~");

				}
				else if (e.Command == Cmd1)
				{

					TouchScreenText += (!ShiftFlag) ? "1" : ShiftMethod("!");

				}
				else if (e.Command == Cmd2)
				{
					TouchScreenText += (!ShiftFlag) ? "2" : ShiftMethod("@");
				}
				else if (e.Command == CmdAtTheRate)
				{
					TouchScreenText += "@";
				}
				else if (e.Command == Cmd3)
				{
					TouchScreenText += (!ShiftFlag) ? "3" : ShiftMethod("#");
				}
				else if (e.Command == Cmd4)
				{
					TouchScreenText += (!ShiftFlag) ? "4" : ShiftMethod("$");
				}
				else if (e.Command == Cmd5)
				{
					TouchScreenText += (!ShiftFlag) ? "5" : ShiftMethod("%");
				}
				else if (e.Command == Cmd6)
				{
					TouchScreenText += (!ShiftFlag) ? "6" : ShiftMethod("^");
				}
				else if (e.Command == Cmd7)
				{
					TouchScreenText += (!ShiftFlag) ? "7" : ShiftMethod("&");
				}
				else if (e.Command == Cmd8)
				{
					TouchScreenText += (!ShiftFlag) ? "8" : ShiftMethod("*");
				}
				else if (e.Command == Cmd9)
				{
					TouchScreenText += (!ShiftFlag) ? "9" : ShiftMethod("(");
				}
				else if (e.Command == Cmd0)
				{
					TouchScreenText += (!ShiftFlag) ? "0" : ShiftMethod(")");
				}
				else if (e.Command == CmdMinus)
				{
					TouchScreenText += (!ShiftFlag) ? "-" : ShiftMethod("_");
				}
				else if (e.Command == CmdHiphen)
				{
					TouchScreenText += "-";

				}
				else if (e.Command == CmdPlus)
				{
					TouchScreenText += "+";
				}
				else if (e.Command == CmdTab)
				{
					TouchScreenText += "     ";
				}
				else if (e.Command == CmdQ)
				{
					AddKeyBoardINput('Q');
				}
				else if (e.Command == Cmdw)
				{
					AddKeyBoardINput('w');
				}
				else if (e.Command == CmdE)
				{
					AddKeyBoardINput('E');
				}
				else if (e.Command == CmdR)
				{
					AddKeyBoardINput('R');
				}
				else if (e.Command == CmdT)
				{
					AddKeyBoardINput('T');
				}
				else if (e.Command == CmdY)
				{
					AddKeyBoardINput('Y');
				}
				else if (e.Command == CmdU)
				{
					AddKeyBoardINput('U');
				}
				else if (e.Command == CmdI)
				{
					AddKeyBoardINput('I');
				}
				else if (e.Command == CmdO)
				{
					AddKeyBoardINput('O');
				}
				else if (e.Command == CmdP)
				{
					AddKeyBoardINput('P');
				}
				else if (e.Command == CmdOpenCrulyBrace)
				{
					TouchScreenText += (!ShiftFlag) ? "[" : ShiftMethod("{");
				}
				else if (e.Command == CmdEndCrultBrace)
				{
					TouchScreenText += (!ShiftFlag) ? "]" : ShiftMethod("}");
				}
				else if (e.Command == CmdOR)
				{
					TouchScreenText += (!ShiftFlag) ? @"\" : ShiftMethod("|");
				}
				else if (e.Command == CmdCapsLock)
				{
					CapsLockFlag = !CapsLockFlag;
				}
				else if (e.Command == CmdA)
				{
					AddKeyBoardINput('A');
				}
				else if (e.Command == CmdS)
				{
					AddKeyBoardINput('S');
				}
				else if (e.Command == CmdD)
				{
					AddKeyBoardINput('D');
				}
				else if (e.Command == CmdF)
				{
					AddKeyBoardINput('F');
				}
				else if (e.Command == CmdG)
				{
					AddKeyBoardINput('G');
				}
				else if (e.Command == CmdH)
				{
					AddKeyBoardINput('H');
				}
				else if (e.Command == CmdJ)
				{
					AddKeyBoardINput('J');
				}
				else if (e.Command == CmdK)
				{
					AddKeyBoardINput('K');
				}
				else if (e.Command == CmdL)
				{
					AddKeyBoardINput('L');
				}
				else if (e.Command == CmdColon)
				{
					TouchScreenText += (!ShiftFlag) ? ";" : ShiftMethod(":");

				}
				else if (e.Command == CmdDoubleInvertedComma)
				{
					TouchScreenText += (!ShiftFlag) ? "'" : ShiftMethod(System.Char.ConvertFromUtf32(34));
				}
				else if (e.Command == CmdShift)
				{
					ShiftFlag = true;
				}
				else if (e.Command == CmdZ)
				{
					AddKeyBoardINput('Z');
				}
				else if (e.Command == CmdX)
				{
					AddKeyBoardINput('X');
				}
				else if (e.Command == CmdC)
				{
					AddKeyBoardINput('C');
				}
				else if (e.Command == CmdV)
				{
					AddKeyBoardINput('V');
				}
				else if (e.Command == CmdB)
				{
					AddKeyBoardINput('B');
				}
				else if (e.Command == CmdN)
				{
					AddKeyBoardINput('N');
				}
				else if (e.Command == CmdM)
				{
					AddKeyBoardINput('M');
				}
				else if (e.Command == CmdLessThan)
				{
					TouchScreenText += (!ShiftFlag) ? "," : ShiftMethod("<");
				}
				else if (e.Command == CmdGreaterThan)
				{
					TouchScreenText += (!ShiftFlag) ? "." : ShiftMethod(">");
				}
				else if (e.Command == CmdQuestion)
				{
					TouchScreenText += (!ShiftFlag) ? "/" : ShiftMethod("?");
				}
				else if (e.Command == CmdSpaceBar)
				{
					TouchScreenText += " ";
				}

			}
			if (e.Command == CmdEnter)
			{
				if (InstanceObject != null)
				{
					InstanceObject.Close();
					InstanceObject = null;
				}

				_CurrentControl.MoveFocus(new System.Windows.Input.TraversalRequest(System.Windows.Input.FocusNavigationDirection.Next));
			}
			else if (e.Command == CmdBackspace)
			{
				if (!string.IsNullOrEmpty(TouchScreenText))
				{
					TouchScreenText = TouchScreenText.Substring(0, TouchScreenText.Length - 1);
				}
			}
			else if (e.Command == CmdClear)
			{
				TouchScreenText = string.Empty;
			}
		}
		private static string ShiftMethod(string ch)
		{
			ShiftFlag = false;
			return ch;
		}
		private static void AddKeyBoardINput(char input)
		{
			if (CapsLockFlag)
			{
				if (ShiftFlag)
				{
					TouchScreenText += char.ToLower(input).ToString();
					ShiftFlag = false;
				}
				else
				{
					TouchScreenText += char.ToUpper(input).ToString();
				}
			}
			else
			{
				if (!ShiftFlag)
				{
					TouchScreenText += char.ToLower(input).ToString();
				}
				else
				{
					TouchScreenText += char.ToUpper(input).ToString();
					ShiftFlag = false;
				}
			}
		}
		#endregion


	}
}
