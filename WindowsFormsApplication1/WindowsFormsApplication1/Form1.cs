using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Media;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {


        public struct Token
        {
            public string Name;
            public string Value;
            public Token(string n, string v)
            {
                Name = n;
                Value = v;
            }
        }
        public struct TokenRegistrateEx
            {
                public string Name;
                public string Regex;
                public TokenRegistrateEx(string N, string R)
                {
                    Name = N;
                    Regex = R;
                }

            }

        public class Tokenizator {
            List<TokenRegistrateEx> RegEx = new List<TokenRegistrateEx>();
            public Tokenizator() {
                RegEx.Add(new TokenRegistrateEx("printT",@"(^print$)"));
                RegEx.Add(new TokenRegistrateEx("openBracketsT", @"(^\($)"));
                RegEx.Add(new TokenRegistrateEx("closeBracketsT", @"(^\)$)"));
                RegEx.Add(new TokenRegistrateEx("varT", @"(^([A-Za-z][A-Za-z0-9]*)$)"));
                RegEx.Add(new TokenRegistrateEx("arOpT", @"(^([+|\-|*|\/])$)"));
                RegEx.Add(new TokenRegistrateEx("digitT", @"(^((-?\d+)(\,\d*)?)$)"));
               // RegEx.Add(new TokenRegistrateEx("digitT", @"{[^}]*""id"":(\d+)[^}]*avail:(-\d+)[^}]*cost:\[(\d+)?\][^}]*"));
                RegEx.Add(new TokenRegistrateEx("equalOpT", @"^(=)$"));
                RegEx.Add(new TokenRegistrateEx("endT", @"(^(;)$)"));
                RegEx.Add(new TokenRegistrateEx("spaceT", @"(^\s$)"));
                RegEx.Add(new TokenRegistrateEx("Error: unknown element", "[$.`'{}<>]"));

            }
            

            public string  TokenIsMatch(string progtext) {
                foreach (TokenRegistrateEx r in RegEx) {
                    if (Regex.IsMatch(progtext, r.Regex)) { return r.Name; break; }
                }
                return null;
            }

            public bool IsSpace(Token s) {
                return Regex.IsMatch(s.Value, @"(^\s$)");
            }
            
            public List<Token> getTokens (string programmText) {
                string buffer1= "";
                string buffer2= "";
                List<Token> ListOfTokens = new List<Token>();

                for (int i=0; i <= programmText.Length -1 ;i++) {
                    buffer2 = buffer1;
                    buffer1 += programmText[i];
                    if (TokenIsMatch(buffer1) == null) {
                        i--;
                        ListOfTokens.Add(new Token(TokenIsMatch(buffer2), buffer2));
                        buffer1 = "";
                    }
                    
                }

                if (TokenIsMatch(buffer1) != null)
                {
                    ListOfTokens.Add(new Token(TokenIsMatch(buffer1), buffer1));
                }
                ListOfTokens.RemoveAll(IsSpace);
                return ListOfTokens;
            }
            
            
        }

        public struct Lexem {
            public string token1;
            public string token2;
            public Lexem(string token1,string token2) {
                this.token1 = token1;
                this.token2 = token2;
            }
        }

        public class Lexer
        {

            List<Lexem> Lexems = new List<Lexem>();

            public Lexer()
            {


                /*RegEx.Add(new TokenRegistrateEx("printT", @"(^print$)"));
                RegEx.Add(new TokenRegistrateEx("openBracketsT", @"(^\($)"));
                RegEx.Add(new TokenRegistrateEx("closeBracketsT", @"(^\)$)"));
                RegEx.Add(new TokenRegistrateEx("varT", @"(^([A-Za-z][A-Za-z0-9]*)$)"));
                RegEx.Add(new TokenRegistrateEx("arOpT", @"(^([+|\-|*|\/])$)"));
                RegEx.Add(new TokenRegistrateEx("digitT", @"(^((-?\d+)(\,\d*)?)$)"));
                RegEx.Add(new TokenRegistrateEx("equalOpT", @"^(=)$"));
                RegEx.Add(new TokenRegistrateEx("endT", @"(^(;)$)"));
                RegEx.Add(new TokenRegistrateEx("spaceT", @"(^\s$)"));*/


                Lexems.Add(new Lexem("printT", "openBracketsT"));

                Lexems.Add(new Lexem("openBracketsT", "varT"));
                Lexems.Add(new Lexem("openBracketsT", "digitT"));
                Lexems.Add(new Lexem("openBracketsT", "spaceT"));

                Lexems.Add(new Lexem("closeBracketsT", "arOpt"));
                Lexems.Add(new Lexem("closeBracketsT", "equalOpT"));
                Lexems.Add(new Lexem("closeBracketsT", "endT"));
                Lexems.Add(new Lexem("closeBracketsT", "spaceT"));

                Lexems.Add(new Lexem("varT", "closeBracketsT"));
                Lexems.Add(new Lexem("varT", "arOpT"));
                Lexems.Add(new Lexem("varT", "equalOpT"));
                Lexems.Add(new Lexem("varT", "endT"));
                Lexems.Add(new Lexem("varT", "spaceT"));

                Lexems.Add(new Lexem("arOpT", "openBracketsT"));
                Lexems.Add(new Lexem("arOpT", "varT"));
                Lexems.Add(new Lexem("arOpT", "digitT"));
                Lexems.Add(new Lexem("arOpT", "spaceT"));

                Lexems.Add(new Lexem("digitT", "closeBracketsT"));
                Lexems.Add(new Lexem("digitT", "arOpT"));
                Lexems.Add(new Lexem("digitT", "endT"));
                Lexems.Add(new Lexem("digit", "SpaceT"));

                Lexems.Add(new Lexem("equalOpT", "openBracketsT"));
                Lexems.Add(new Lexem("equalOpT", "varT"));
                Lexems.Add(new Lexem("equalOpT", "digitT"));
                Lexems.Add(new Lexem("equalOpT", "SpaceT"));

                Lexems.Add(new Lexem("endT", "printT"));
                Lexems.Add(new Lexem("end", "openBracketsT"));
                Lexems.Add(new Lexem("end", "varT"));
                Lexems.Add(new Lexem("end", "spaceT"));

                Lexems.Add(new Lexem("spaceT", "printT"));
                Lexems.Add(new Lexem("spaceT", "openBracketsT"));
                Lexems.Add(new Lexem("spaceT", "closeBracketsT"));
                Lexems.Add(new Lexem("spaceT", "varT"));
                Lexems.Add(new Lexem("spaceT", "arOpT"));
                Lexems.Add(new Lexem("spaceT", "digitT"));
                Lexems.Add(new Lexem("spaceT", "equalOpT"));
                Lexems.Add(new Lexem("spaceT", "endT"));
            }

            private bool IsRightLexem(Token t1, Token t2)
            {

                foreach (Lexem l in Lexems)
                {
                    if ((t1.Name == l.token1) && (t2.Name == l.token2))
                    {
                        return true;

                    }
                }
                return false;

            }

            public bool Lexe(List<Token> Tokens)
            {
                if (Tokens == null) { return true; }
                if (Tokens[0].Name == "Error: unknown element") { return false; }
                for (int i = 0; i <= Tokens.Count - 2; i++)
                {
                    if (IsRightLexem(Tokens[i], Tokens[i + 1]) == false)
                        return false;

                }
                return true;

            }

            public bool BracketsRight(List<Token> Tokens)
            {
                int openBracketsT = 0, closeBracketsT = 0;
                foreach (Token tk in Tokens)
                {
                    switch (tk.Name)
                    {
                        case "openBracketsT": { openBracketsT++; break; }
                        case "closeBracketsT": { closeBracketsT++; break; }
                        default: break;
                    }
                }

                if (openBracketsT == closeBracketsT)  return true;
                else return false;

            }


        }

        public class Polskalizator {                                       //making polish string from the list of token's

            public int hmpriority(Token t) {                               //calculate priority for tokens
                if (t.Value == "/" || t.Value == "*") return 10;
                if (t.Value == "+" || t.Value == "-") return 9;
                if (t.Value == "="||t.Value== "print") return 8;
                else return 0;
            }

            public List<Token> getpolsk (List<Token> nepolsk) {            //return polish string from non-polish string
                bool toprint = false;
                List<Token> alreadyPolsk = new List<Token>();
                Stack<Token> stack = new Stack<Token>();
                foreach (Token token in nepolsk) {
                    if (token.Name == "printT") toprint = true;
                    else if (token.Name == "endT") while (stack.Count != 0) alreadyPolsk.Add(stack.Pop());
                    else if (token.Name == "digitT" || token.Name == "varT") alreadyPolsk.Add(token);
                    else if (token.Name == "openBracketsT") stack.Push(token);
                    else if (token.Name == "closeBracketsT")
                    {
                        while (stack.Peek().Name != "openBracketsT" || stack.Count == 0) alreadyPolsk.Add(stack.Pop());
                        stack.Pop();
                    }
                    else if (token.Name == "arOpT" || token.Name == "equalOpT")
                    {
                        if (stack.Count == 0) { stack.Push(token); }
                        else if (stack.Peek().Name == "openBracketsT") stack.Push(token);
                        else if (hmpriority(token) <= hmpriority(stack.Peek()))
                        {
                            while (stack.Count != 0 && hmpriority(token) <= hmpriority(stack.Peek()))
                            {
                                if (stack.Peek().Name == "openBracketsT") break;
                                alreadyPolsk.Add(stack.Pop());
                            }
                            stack.Push(token);
                        }
                        else if (hmpriority(token) > hmpriority(stack.Peek())) stack.Push(token);
                    }

                }
                if (toprint) alreadyPolsk.Add(new Token("printT", "print"));

                while (stack.Count != 0) {
                    alreadyPolsk.Add(stack.Pop());
                }
                return alreadyPolsk;

            }
        }

        public struct VarHolder {                          //struct to intrepret tokens to VM
            public double value;
            public string name;

           public VarHolder(string name, double value) {
                this.name = name;
                this.value = value;
            }
        }

        public class Machine
        {
            private Stack<VarHolder> stack = new Stack<VarHolder>();
            private List<VarHolder> vars = new List<VarHolder>();
            bool IsExist(string var)
            {
                foreach (VarHolder v in vars)
                {
                    if (v.name == var) return true;

                }
                return false;
            }

            double GetVarHolderByName(string name)
            {
                foreach (VarHolder var in vars)
                {
                    if (var.name == name) return var.value;
                }
                return 0f;
            }

            int GetVarHolderByNameIndex(string name)
            {
                for (int i = 0; i < vars.Count; i++)
                {
                    if (vars[i].name == name) return i;
                }
                return -1;
            }
            public string vmachine(List<Token> polskStroka)
            {

                string output = "";
                foreach (Token token in polskStroka)
                {
                    switch (token.Name)
                    {


                        /*RegEx.Add(new TokenRegistrateEx("printT", @"(^print$)"));
                    RegEx.Add(new TokenRegistrateEx("openBracketsT", @"(^\($)"));
                    RegEx.Add(new TokenRegistrateEx("closeBracketsT", @"(^\)$)"));
                    RegEx.Add(new TokenRegistrateEx("varT", @"(^([A-Za-z][A-Za-z0-9]*)$)"));
                    RegEx.Add(new TokenRegistrateEx("arOpT", @"(^([+|\-|*|\/])$)"));
                    RegEx.Add(new TokenRegistrateEx("digitT", @"(^((-?\d+)(\,\d*)?)$)"));
                    RegEx.Add(new TokenRegistrateEx("equalOpT", @"^(=)$"));
                    RegEx.Add(new TokenRegistrateEx("endT", @"(^(;)$)"));
                    RegEx.Add(new TokenRegistrateEx("spaceT", @"(^\s$)"));*/
                        case "printT":
                            output += stack.Pop().value;
                            break;
                        case "varT":
                            {
                                if (!IsExist(token.Value))
                                {
                                    stack.Push(new VarHolder(token.Value, 0f));
                                    vars.Add(new VarHolder(token.Value, 0f));
                                }
                                else
                                {
                                    stack.Push(new VarHolder(token.Value, GetVarHolderByName(token.Value)));
                                    

                                }
                            }
                            break;
                        case "digitT":
                            stack.Push(new VarHolder("", Convert.ToDouble(token.Value)));
                            break;

                        case "arOpT":
                            {
                                double op1 = stack.Pop().value;
                                double op2 = stack.Pop().value;
                                if (token.Value == "+")
                                {
                                    stack.Push(new VarHolder("", (op1 + op2)));
                                }
                                else if (token.Value == "-")
                                {
                                    stack.Push(new VarHolder("", (op2 - op1)));
                                }
                                else if (token.Value == "*")
                                {
                                    stack.Push(new VarHolder("", (op1 * op2)));
                                }
                                else if (token.Value == "/")
                                {
                                    stack.Push(new VarHolder("", (op1 / op2)));
                                }
                            }
                            break;
                        case "equalOpT":
                            {
                                double opb = stack.Pop().value;
                                string opa = stack.Pop().name;
                                int a = (GetVarHolderByNameIndex(opa));
                                vars[a] = new VarHolder("", opb);
                            }
                            break;

                        case "endT": { stack.Clear(); break; }
                        default: { break; }

                    }
                }
                return output;
            }
        }


        public Form1()
        {
            InitializeComponent();
            playSimpleSound();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*Tokenizator tokenizator = new Tokenizator();
            List<Token> tks = tokenizator.getTokens(ProgrammText.Text);
            foreach (Token t in tks) {
                Tokens.Text += t.Value + ",";

            }*/

           
        }
        private void playSimpleSound()
        {
            System.Media.SoundPlayer simpleSound = new SoundPlayer(@"d:\Music.wav");
            simpleSound.Play();
        }


        private void Letuchka_Click(object sender, EventArgs e)
        {

            Tokenizator tokenezator = new Tokenizator();
            Lexer lexer = new Lexer();
            Tokens.Text = "";
            Output.Text = "";
            PolskaVudkaDobrovudka.Text = "";
            List <Token> tks = tokenezator.getTokens(ProgrammText.Text);

            foreach (Token t in tks){
                Tokens.Text += t.Name + ", ";
            }

            if (lexer.BracketsRight(tks)){

                if (lexer.Lexe(tks))
                {

                    Polskalizator polskalizator = new Polskalizator();
                    List<Token> polis = polskalizator.getpolsk(tks);

                    foreach (Token t in polis)
                    {
                        PolskaVudkaDobrovudka.Text += t.Value + ", ";
                    }

                    Machine machine = new Machine();
                    Output.Text = machine.vmachine(polis);
                }
                else Output.Text = "There is some trouble's whith UR sinthax. PLS check it";
            }
            else Output.Text = "U have not equal amount of brackets";
        }
    }
}
