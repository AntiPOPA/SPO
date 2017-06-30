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

namespace AntiPOPA
{
    public partial class Laba4 : Form
    {


        public struct Token
        {
            public string Imya;
            public string ZnachImeni;
            public Token(string u, string z)
            {
                Imya = u;
                ZnachImeni = z;
            }
        }
        public struct TokenEnrollment
            {
                public string Imya;
                public string TEnroll;
                public TokenEnrollment(string U, string E)
                {
                    Imya = U;
                    TEnroll = E;
                }

            }

        public class Tokenizator {
            List<TokenEnrollment> TENROLL = new List<TokenEnrollment>();
            public Tokenizator() {
                TENROLL.Add(new TokenEnrollment("printT",@"(^print$)"));
                TENROLL.Add(new TokenEnrollment("openBracketsT", @"(^\($)"));
                TENROLL.Add(new TokenEnrollment("closeBracketsT", @"(^\)$)"));
                TENROLL.Add(new TokenEnrollment("varT", @"(^([A-Za-z][A-Za-z0-9]*)$)"));
                TENROLL.Add(new TokenEnrollment("arOpT", @"(^([+|\-|*|\/])$)"));
                TENROLL.Add(new TokenEnrollment("digitT", @"(^((-?\d+)(\,\d*)?)$)"));
                TENROLL.Add(new TokenEnrollment("equalOpT", @"^(=)$"));
                TENROLL.Add(new TokenEnrollment("endT", @"(^(;)$)"));
                TENROLL.Add(new TokenEnrollment("spaceT", @"(^\s$)"));
                TENROLL.Add(new TokenEnrollment("Error: unknown element", "[$.`'{}<>]"));

            }
            

            public string  TokenIsMatch(string progtext) {
                foreach (TokenEnrollment r in TENROLL) {
                    if (Regex.IsMatch(progtext, r.TEnroll)) { return r.Imya; break; }
                }
                return null;
            }

            public bool IsSpace(Token s) {
                return Regex.IsMatch(s.ZnachImeni, @"(^\s$)");
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
                    if ((t1.Imya == l.token1) && (t2.Imya == l.token2))
                    {
                        return true;

                    }
                }
                return false;

            }

            public bool Lexe(List<Token> Tokens)
            {
                if (Tokens == null) { return true; }
                if (Tokens[0].Imya == "Error: unknown element") { return false; }
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
                    switch (tk.Imya)
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
                if (t.ZnachImeni == "/" || t.ZnachImeni == "*") return 10;
                if (t.ZnachImeni == "+" || t.ZnachImeni == "-") return 9;
                if (t.ZnachImeni == "="||t.ZnachImeni== "print") return 8;
                else return 0;
            }

            public List<Token> getpolsk (List<Token> nepolsk) {            //return polish string from non-polish string
                bool toprint = false;
                List<Token> alreadyPolsk = new List<Token>();
                Stack<Token> stack = new Stack<Token>();
                foreach (Token token in nepolsk) {
                    if (token.Imya == "printT") toprint = true;
                    else if (token.Imya == "endT") while (stack.Count != 0) alreadyPolsk.Add(stack.Pop());
                    else if (token.Imya == "digitT" || token.Imya == "varT") alreadyPolsk.Add(token);
                    else if (token.Imya == "openBracketsT") stack.Push(token);
                    else if (token.Imya == "closeBracketsT")
                    {
                        while (stack.Peek().Imya != "openBracketsT" || stack.Count == 0) alreadyPolsk.Add(stack.Pop());
                        stack.Pop();
                    }
                    else if (token.Imya == "arOpT" || token.Imya == "equalOpT")
                    {
                        if (stack.Count == 0) { stack.Push(token); }
                        else if (stack.Peek().Imya == "openBracketsT") stack.Push(token);
                        else if (hmpriority(token) <= hmpriority(stack.Peek()))
                        {
                            while (stack.Count != 0 && hmpriority(token) <= hmpriority(stack.Peek()))
                            {
                                if (stack.Peek().Imya == "openBracketsT") break;
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
                    switch (token.Imya)
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
                                if (!IsExist(token.ZnachImeni))
                                {
                                    stack.Push(new VarHolder(token.ZnachImeni, 0f));
                                    vars.Add(new VarHolder(token.ZnachImeni, 0f));
                                }
                                else
                                {
                                    stack.Push(new VarHolder(token.ZnachImeni, GetVarHolderByName(token.ZnachImeni)));
                                    

                                }
                            }
                            break;
                        case "digitT":
                            stack.Push(new VarHolder("", Convert.ToDouble(token.ZnachImeni)));
                            break;

                        case "arOpT":
                            {
                                double op1 = stack.Pop().value;
                                double op2 = stack.Pop().value;
                                if (token.ZnachImeni == "+")
                                {
                                    stack.Push(new VarHolder("", (op1 + op2)));
                                }
                                else if (token.ZnachImeni == "-")
                                {
                                    stack.Push(new VarHolder("", (op2 - op1)));
                                }
                                else if (token.ZnachImeni == "*")
                                {
                                    stack.Push(new VarHolder("", (op1 * op2)));
                                }
                                else if (token.ZnachImeni == "/")
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


        public Laba4()
        {
            InitializeComponent();
           // playSimpleSound();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*Tokenizator tokenizator = new Tokenizator();
            List<Token> tks = tokenizator.getTokens(ProgrammText.Text);
            foreach (Token t in tks) {
                Tokens.Text += t.Value + ",";

            }*/

           
        }
       /* private void playSimpleSound()
        {
            System.Media.SoundPlayer simpleSound = new SoundPlayer(@"d:\Music.wav");
            simpleSound.Play();
        }*/


        private void Letuchka_Click(object sender, EventArgs e)
        {

            Tokenizator tokenezator = new Tokenizator();
            Lexer lexer = new Lexer();
            Tokens.Text = "";
            Output.Text = "";
            PolskaVudkaDobrovudka.Text = "";
            List <Token> tks = tokenezator.getTokens(ProgrammText.Text);

            foreach (Token t in tks){
                Tokens.Text += t.Imya + ", ";
            }

            if (lexer.BracketsRight(tks)){

                if (lexer.Lexe(tks))
                {

                    Polskalizator polskalizator = new Polskalizator();
                    List<Token> polis = polskalizator.getpolsk(tks);

                    foreach (Token t in polis)
                    {
                        PolskaVudkaDobrovudka.Text += t.ZnachImeni + ", ";
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
