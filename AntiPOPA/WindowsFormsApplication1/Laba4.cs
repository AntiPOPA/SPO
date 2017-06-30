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

        public class TokenSlowar {
            List<TokenEnrollment> TENROLL = new List<TokenEnrollment>();
            public TokenSlowar() {
                TENROLL.Add(new TokenEnrollment("pechat",@"(^print$)"));
                TENROLL.Add(new TokenEnrollment("leftscobka", @"(^\($)"));
                TENROLL.Add(new TokenEnrollment("rightscobka", @"(^\)$)"));
                TENROLL.Add(new TokenEnrollment("peremen", @"(^([A-Za-z][A-Za-z0-9]*)$)"));
                TENROLL.Add(new TokenEnrollment("znakiAR", @"(^([+|\-|*|\/])$)"));
                TENROLL.Add(new TokenEnrollment("chisl", @"(^((-?\d+)(\,\d*)?)$)"));
                TENROLL.Add(new TokenEnrollment("ravno", @"^(=)$"));
                TENROLL.Add(new TokenEnrollment("konecstr", @"(^(;)$)"));
                TENROLL.Add(new TokenEnrollment("inane", @"(^\s$)"));
                TENROLL.Add(new TokenEnrollment("This isn't good", "[$.`'{}<>]"));

            }
            

            public string  Sravnenie(string txt) {
                foreach (TokenEnrollment t in TENROLL) {
                    if (Regex.IsMatch(txt, t.TEnroll)) { return t.Imya; break; }
                }
                return null;
            }

            public bool Inane(Token p) {
                return Regex.IsMatch(p.ZnachImeni, @"(^\s$)");
            }
            
            public List<Token> getTokens (string uText) {
                string carrier0= "";
                string carrier= "";
                List<Token> ListOT = new List<Token>();
                
                for (int i=0; i <= uText.Length -1 ;i++) {
                    carrier = carrier0;
                    carrier0 += uText[i];
                    if (Sravnenie(carrier0) == null) {
                        i--;
                        ListOT.Add(new Token(Sravnenie(carrier), carrier));
                        carrier0 = "";
                    }
                    
                }

                if (Sravnenie(carrier0) != null)
                {
                    ListOT.Add(new Token(Sravnenie(carrier0), carrier0));
                }
                ListOT.RemoveAll(Inane);
                return ListOT;
            }
            
            
        }

        public struct Lexem {
            public string token0;
            public string token1;
            public Lexem(string token1,string token2) {
                this.token0 = token1;
                this.token1 = token2;
            }
        }

        public class Lexer
        {

            List<Lexem> Lexeme = new List<Lexem>();

            public Lexer()
            {


                

                Lexeme.Add(new Lexem("pechat", "leftscobka"));

                Lexeme.Add(new Lexem("leftscobka", "peremen"));
                Lexeme.Add(new Lexem("leftscobka", "chisl"));
                Lexeme.Add(new Lexem("leftscobka", "inane"));

                Lexeme.Add(new Lexem("rightscobka", "znakiAR"));
                Lexeme.Add(new Lexem("rightscobka", "ravno"));
                Lexeme.Add(new Lexem("rightscobka", "konecstr"));
                Lexeme.Add(new Lexem("rightscobka", "inane"));

                Lexeme.Add(new Lexem("peremen", "rightscobka"));
                Lexeme.Add(new Lexem("peremen", "znakiAR"));
                Lexeme.Add(new Lexem("peremen", "ravno"));
                Lexeme.Add(new Lexem("peremen", "konecstr"));
                Lexeme.Add(new Lexem("peremen", "inane"));

                Lexeme.Add(new Lexem("znakiAR", "leftscobka"));
                Lexeme.Add(new Lexem("znakiAR", "peremen"));
                Lexeme.Add(new Lexem("znakiAR", "chisl"));
                Lexeme.Add(new Lexem("znakiAR", "inane"));

                Lexeme.Add(new Lexem("chisl", "rightscobka"));
                Lexeme.Add(new Lexem("chisl", "znakiAR"));
                Lexeme.Add(new Lexem("chisl", "konecstr"));
                Lexeme.Add(new Lexem("chisl", "inane"));

                Lexeme.Add(new Lexem("ravno", "leftscobka"));
                Lexeme.Add(new Lexem("ravno", "peremen"));
                Lexeme.Add(new Lexem("ravno", "chisl"));
                Lexeme.Add(new Lexem("ravno", "inane"));

                Lexeme.Add(new Lexem("konecstr", "pechat"));
                Lexeme.Add(new Lexem("konecstr", "leftscobka"));
                Lexeme.Add(new Lexem("konecstr", "peremen"));
                Lexeme.Add(new Lexem("konecstr", "inane"));

                Lexeme.Add(new Lexem("inane", "pechat"));
                Lexeme.Add(new Lexem("inane", "leftscobka"));
                Lexeme.Add(new Lexem("inane", "rightscobka"));
                Lexeme.Add(new Lexem("inane", "peremen"));
                Lexeme.Add(new Lexem("inane", "znakiAR"));
                Lexeme.Add(new Lexem("inane", "chisl"));
                Lexeme.Add(new Lexem("inane", "ravno"));
                Lexeme.Add(new Lexem("inane", "konecstr"));
            }

            private bool ProvLexem(Token t0, Token t1)
            {

                foreach (Lexem L in Lexeme)
                {
                    if ((t0.Imya == L.token0) && (t1.Imya == L.token1))
                    {
                        return true;

                    }
                }
                return false;

            }

            public bool Lexe(List<Token> Tokens)
            {
                if (Tokens == null) { return true; }
                if (Tokens[0].Imya == "This isn't good") { return false; }
                for (int i = 0; i <= Tokens.Count - 2; i++)
                {
                    if (ProvLexem(Tokens[i], Tokens[i + 1]) == false)
                        return false;

                }
                return true;

            }

            public bool ProvScobka(List<Token> Tokens)
            {
                int leftscobka = 0, rightscobka = 0;
                foreach (Token tk in Tokens)
                {
                    switch (tk.Imya)
                    {
                        case "leftscobka": { leftscobka++; break; }
                        case "rightscobka": { rightscobka++; break; }
                        default: break;
                    }
                }

                if (leftscobka == rightscobka)  return true;
                else return false;

            }


        }

        public class PolskiyStrEditor {                                       //making polish string from the list of token's

            public int precedency(Token t) {                               //calculate priority for tokens
                if (t.ZnachImeni == "/" || t.ZnachImeni == "*") return 10;
                if (t.ZnachImeni == "+" || t.ZnachImeni == "-") return 9;
                if (t.ZnachImeni == "="||t.ZnachImeni== "print") return 8;
                else return 0;
            }

            public List<Token> getpolsk (List<Token> notpolsk) {            //return polish string from non-polish string
                bool toprint = false;
                List<Token> alreadyPolsk = new List<Token>();
                Stack<Token> stack = new Stack<Token>();
                foreach (Token token in notpolsk) {
                    if (token.Imya == "pechat") toprint = true;
                    else if (token.Imya == "konecstr") while (stack.Count != 0) alreadyPolsk.Add(stack.Pop());
                    else if (token.Imya == "chisl" || token.Imya == "peremen") alreadyPolsk.Add(token);
                    else if (token.Imya == "leftscobka") stack.Push(token);
                    else if (token.Imya == "rightscobka")
                    {
                        while (stack.Peek().Imya != "leftscobka" || stack.Count == 0) alreadyPolsk.Add(stack.Pop());
                        stack.Pop();
                    }
                    else if (token.Imya == "znakiAR" || token.Imya == "ravno")
                    {
                        if (stack.Count == 0) { stack.Push(token); }
                        else if (stack.Peek().Imya == "leftscobka") stack.Push(token);
                        else if (precedency(token) <= precedency(stack.Peek()))
                        {
                            while (stack.Count != 0 && precedency(token) <= precedency(stack.Peek()))
                            {
                                if (stack.Peek().Imya == "leftscobka") break;
                                alreadyPolsk.Add(stack.Pop());
                            }
                            stack.Push(token);
                        }
                        else if (precedency(token) > precedency(stack.Peek())) stack.Push(token);
                    }

                }
                if (toprint) alreadyPolsk.Add(new Token("pechat", "print"));

                while (stack.Count != 0) {
                    alreadyPolsk.Add(stack.Pop());
                }
                return alreadyPolsk;

            }
        }

        public struct VarHolder {                          //struct to intrepret tokens to VM
            public double znachenieimen;
            public string imya;

           public VarHolder(string imya, double znachenieimen) {
                this.imya = imya;
                this.znachenieimen = znachenieimen;
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
                    if (v.imya == var) return true;

                }
                return false;
            }

            double GetVarHolderByName(string name)
            {
                foreach (VarHolder var in vars)
                {
                    if (var.imya == name) return var.znachenieimen;
                }
                return 0f;
            }

            int GetVarHolderByNameIndex(string name)
            {
                for (int i = 0; i < vars.Count; i++)
                {
                    if (vars[i].imya == name) return i;
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


                        
                        case "pechat":
                            output += stack.Pop().znachenieimen;
                            break;
                        case "peremen":
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
                        case "chisl":
                            stack.Push(new VarHolder("", Convert.ToDouble(token.ZnachImeni)));
                            break;

                        case "znakiAR":
                            {
                                double op1 = stack.Pop().znachenieimen;
                                double op2 = stack.Pop().znachenieimen;
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
                        case "ravno":
                            {
                                double opb = stack.Pop().znachenieimen;
                                string opa = stack.Pop().imya;
                                int a = (GetVarHolderByNameIndex(opa));
                                vars[a] = new VarHolder("", opb);
                            }
                            break;

                        case "konecstr": { stack.Clear(); break; }
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
            
        }      


        private void Letuchka_Click(object sender, EventArgs e)
        {

            TokenSlowar tokenezator = new TokenSlowar();
            Lexer lexer = new Lexer();
            Tokens.Text = "";
            Output.Text = "";
            PolskaVudkaDobrovudka.Text = "";
            List <Token> tks = tokenezator.getTokens(ProgrammText.Text);

            foreach (Token t in tks){
                Tokens.Text += t.Imya + ", ";
            }

            if (lexer.ProvScobka(tks)){

                if (lexer.Lexe(tks))
                {

                    PolskiyStrEditor polskalizator = new PolskiyStrEditor();
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
