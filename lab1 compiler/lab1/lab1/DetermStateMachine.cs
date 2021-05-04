using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stateless;

namespace lab1
{
    enum States
    {
        q1,
        q2,
        q31, q32, q33, q34, q35, q36, q37,
        q41, q42, q43, q44, q45,
        q5
    }
    enum Triggers
    {
        CapitalLetters,
        LowercaseLetters,
        Space,
        Dot,
        Dash,
        Zapitaya
    }
    class DetermStateMachine
    {
        private StateMachine<States, Triggers> stateMachine;
        public string str { get; set; }
        char preSymb;
        public string states { set; get; } = "q1->";
        public DetermStateMachine()
        {
            stateMachine = new StateMachine<States, Triggers>(States.q1);
            stateMachine.Configure(States.q1).Permit(Triggers.CapitalLetters, States.q2)
                .OnActivate(() => states += "q1->");
            stateMachine.Configure(States.q2).Permit(Triggers.Dot, States.q41)
                .Permit(Triggers.LowercaseLetters, States.q31)
                .OnEntry(() => str = str.Substring(str.Length-1))
                .OnEntry(() => states+="q2->" );
            stateMachine.Configure(States.q31).Permit(Triggers.Space, States.q34)
                .Permit(Triggers.Dash, States.q32)
                .OnEntry(() => states += "q31->");
            stateMachine.Configure(States.q32).Permit(Triggers.CapitalLetters, States.q33)
                .OnEntry(() => states += "q32->");
            stateMachine.Configure(States.q33).Permit(Triggers.Space, States.q34)
                .OnEntry(() => states += "q33->");
            stateMachine.Configure(States.q34).Permit(Triggers.CapitalLetters, States.q35)
                .OnEntry(() => states += "q34->");
            stateMachine.Configure(States.q35).Permit(Triggers.Dot, States.q36)
                .Permit(Triggers.LowercaseLetters, States.q2)
                .OnEntry(() => states += "q35->");
            stateMachine.Configure(States.q36).Permit(Triggers.CapitalLetters, States.q37)
                .OnEntry(() => states += "q36->");
            stateMachine.Configure(States.q37).Permit(Triggers.Dot, States.q5)
                .OnEntry(() => states += "q37->") .OnExit(() => states += "q5");
            stateMachine.Configure(States.q41).Permit(Triggers.CapitalLetters, States.q42).OnEntry(() => states += "q41->");
            stateMachine.Configure(States.q42).Permit(Triggers.Dot, States.q43).OnEntry(() => states += "q42->");
            stateMachine.Configure(States.q43).Permit(Triggers.CapitalLetters, States.q44)
                .Permit(Triggers.Space, States.q44).OnEntry(() => states += "q43->");
            stateMachine.Configure(States.q44).Permit(Triggers.Dash, States.q45)
                .Permit(Triggers.Space, States.q5).OnEntry(() => states += "q44->")
                .Permit(Triggers.Dot, States.q5)
                .Permit(Triggers.Zapitaya, States.q5)
                .OnExit(() => states += "q5");
            stateMachine.Configure(States.q45).Permit(Triggers.CapitalLetters, States.q44);
        }
        public void CapitalLetters()
        {
            if (stateMachine.CanFire(Triggers.CapitalLetters))
                stateMachine.Fire(Triggers.CapitalLetters);
        }
        public void LowercaseLetters()
        {
            if (stateMachine.CanFire(Triggers.LowercaseLetters))
                stateMachine.Fire(Triggers.LowercaseLetters);
        }
        public void Space()
        {
            if (stateMachine.CanFire(Triggers.Space))
                stateMachine.Fire(Triggers.Space);
            else str = "";
        }
        public void Dot()
        {
            if (stateMachine.CanFire(Triggers.Dot))
                stateMachine.Fire(Triggers.Dot);
            else str = "";
        }
        public void Dash()
        {
            if (stateMachine.CanFire(Triggers.Dash))
                stateMachine.Fire(Triggers.Dash);
            else str = "";
        }
        void Zap()
        {
            if (stateMachine.CanFire(Triggers.Zapitaya))
                stateMachine.Fire(Triggers.Zapitaya);
            else str = "";
        }
        public States getState()
        {
            var q = stateMachine.State;
            return q;
        }
        public string getStr() => str;
        public bool SymbolChecking(char symbol)
        {
            int kode = (int)symbol;
            str += symbol;
            if (str.Length>1)
                preSymb = str[str.Length - 2];
            //if (symbol == '\n') str = "";
            if ((kode >= 1040 && kode <= 1071) || kode == 1025)
            {
                if (stateMachine.State == States.q31)
                    str = symbol.ToString();
                CapitalLetters();
            }
            else if ((kode >= 1072 && kode <= 1103) || kode == 1105)
                LowercaseLetters();
            else if (symbol == ' ') Space();
            else if (symbol == '.') Dot();
            else if (symbol == '-') Dash();
            else if (symbol == ',') Zap();
            else
            {
                str = "";
                return false;
            }
            return true;
        }
    }
}
