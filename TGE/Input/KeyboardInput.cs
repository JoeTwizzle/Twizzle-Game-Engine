using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Text;

namespace TGE
{
    public class KeyboardInput
    {
        public string Text = "";
        public bool IsShifted = false;
        public int MaxLength = 10000;
        public bool IsActive = true;
        int cont = 20;
        int freq = 20;
        public void Update()
        {
            if (IsActive)
            {
                foreach (var item in Input.keyboardUpdate)
                {
                    if (item.IsPressed)
                    {
                        if (item.Key == Key.LeftShift || item.Key == Key.RightShift)
                        {
                            IsShifted = true;
                        }
                        Key key = item.Key;
                        string data = "";
                        switch (key)
                        {
                            case Key.Escape:
                                break;
                            case Key.D1:
                                data = "1";
                                break;
                            case Key.D2:
                                data = "2";
                                break;
                            case Key.D3:
                                data = "3";
                                break;
                            case Key.D4:
                                data = "4";
                                break;
                            case Key.D5:
                                data = "5";
                                break;
                            case Key.D6:
                                data = "6";
                                break;
                            case Key.D7:
                                data = "7";
                                break;
                            case Key.D8:
                                data = "8";
                                break;
                            case Key.D9:
                                data = "9";
                                break;
                            case Key.D0:
                                data = "0";
                                break;
                            case Key.Back:
                                if (Text.Length > 0)
                                {
                                    Text = Text.Remove(Text.Length - 1, 1);
                                    cont = freq;
                                }
                                break;
                            case Key.Tab:
                                data = "   ";
                                break;
                            case Key.LeftBracket:
                                break;
                            case Key.RightBracket:
                                break;
                            case Key.Return:
                                break;
                            case Key.LeftControl:
                                break;
                            case Key.LeftShift:
                                break;
                            case Key.RightShift:
                                break;
                            case Key.LeftAlt:
                                break;
                            case Key.Space:
                                data = " ";
                                break;
                            case Key.Capital:
                                //Capslock
                                IsShifted = !IsShifted;
                                break;
                            case Key.F1:
                                break;
                            case Key.F2:
                                break;
                            case Key.F3:
                                break;
                            case Key.F4:
                                break;
                            case Key.F5:
                                break;
                            case Key.F6:
                                break;
                            case Key.F7:
                                break;
                            case Key.F8:
                                break;
                            case Key.F9:
                                break;
                            case Key.F10:
                                break;
                            case Key.NumberLock:
                                break;
                            case Key.ScrollLock:
                                break;
                            case Key.NumberPad7:
                                break;
                            case Key.NumberPad8:
                                break;
                            case Key.NumberPad9:
                                break;
                            case Key.NumberPad4:
                                break;
                            case Key.NumberPad5:
                                break;
                            case Key.NumberPad6:
                                break;
                            case Key.NumberPad1:
                                break;
                            case Key.NumberPad2:
                                break;
                            case Key.NumberPad3:
                                break;
                            case Key.NumberPad0:
                                break;
                            case Key.Decimal:
                                break;
                            case Key.Oem102:
                                break;
                            case Key.F11:
                                break;
                            case Key.F12:
                                break;
                            case Key.F13:
                                break;
                            case Key.F14:
                                break;
                            case Key.F15:
                                break;
                            case Key.Kana:
                                break;
                            case Key.AbntC1:
                                break;
                            case Key.Convert:
                                break;
                            case Key.NoConvert:
                                break;
                            case Key.AbntC2:
                                break;
                            case Key.NumberPadEquals:
                                break;
                            case Key.PreviousTrack:
                                break;
                            case Key.AT:
                                break;
                            case Key.Kanji:
                                break;
                            case Key.Stop:
                                break;
                            case Key.AX:
                                break;
                            case Key.Unlabeled:
                                break;
                            case Key.NextTrack:
                                break;
                            case Key.NumberPadEnter:
                                break;
                            case Key.RightControl:
                                break;
                            case Key.Mute:
                                break;
                            case Key.Minus:
                                data = "-";
                                break;
                            case Key.Calculator:
                                break;
                            case Key.PlayPause:
                                break;
                            case Key.MediaStop:
                                break;
                            case Key.VolumeDown:
                                break;
                            case Key.VolumeUp:
                                break;
                            case Key.WebHome:
                                break;
                            case Key.NumberPadComma:
                                break;
                            case Key.PrintScreen:
                                break;
                            case Key.RightAlt:
                                break;
                            case Key.Pause:
                                break;
                            case Key.Home:
                                break;
                            case Key.Up:
                                break;
                            case Key.PageUp:
                                break;
                            case Key.Left:
                                break;
                            case Key.Right:
                                break;
                            case Key.End:
                                break;
                            case Key.Down:
                                break;
                            case Key.PageDown:
                                break;
                            case Key.Insert:
                                break;
                            case Key.Delete:
                                break;
                            case Key.LeftWindowsKey:
                                break;
                            case Key.RightWindowsKey:
                                break;
                            case Key.Applications:
                                break;
                            case Key.Power:
                                break;
                            case Key.Sleep:
                                break;
                            case Key.Wake:
                                break;
                            case Key.WebSearch:
                                break;
                            case Key.WebFavorites:
                                break;
                            case Key.WebRefresh:
                                break;
                            case Key.WebStop:
                                break;
                            case Key.WebForward:
                                break;
                            case Key.WebBack:
                                break;
                            case Key.MyComputer:
                                break;
                            case Key.Mail:
                                break;
                            case Key.MediaSelect:
                                break;
                            case Key.Unknown:
                                break;
                            case Key.Equals:
                                data = "/";
                                break;
                            case Key.Add:
                                data = "+";
                                break;
                            case Key.Subtract:
                                data = "-";
                                break;
                            case Key.Multiply:
                                data = "*";
                                break;
                            case Key.Divide:
                                data = "/";
                                break;
                            case Key.Slash:
                                data = "/";
                                break;
                            case Key.Backslash:
                                data = "\\";
                                break;
                            case Key.Period:
                                data = ".";
                                break;
                            case Key.Colon:
                                data = ":";
                                break;
                            case Key.Comma:
                                data = ",";
                                break;
                            case Key.Semicolon:
                                data = ";";
                                break;
                            default:
                                data = key.ToString();
                                break;
                        }
                        //shift
                        if (Text.Length + data.Length < MaxLength)
                        {
                            if (IsShifted)
                            {
                                Text += data.ToUpper();
                            }
                            else
                            {
                                Text += data.ToLower();
                            }
                        }
                    }
                    //shift
                    if (item.IsReleased)
                    {
                        if (item.Key == Key.LeftShift || item.Key == Key.RightShift)
                        {
                            IsShifted = false;
                        }
                    }
                }
                //Backspace
                if (Input.GetKey(Key.Back).Held)
                {
                    cont--;
                    if (Text.Length > 0 && cont <= 0)
                    {
                        Text = Text.Remove(Text.Length - 1, 1);
                        cont = freq;
                    }
                }
            }
        }
    }
}
