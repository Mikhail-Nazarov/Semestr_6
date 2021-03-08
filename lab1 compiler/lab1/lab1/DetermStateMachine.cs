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
        q3,
        q4,
        q5,
        q6,
        q7
    }
    enum Triggers
    {
        CapitalLetters,
        LowercaseLetters,
        Space,
        Dot,
        Dash
    }
    class DetermStateMachine
    {
        private StateMachine<States, Triggers> stateMachine;
        string str;
        public DetermStateMachine()
        {
            stateMachine = new StateMachine<States, Triggers>(States.q1);
            stateMachine.Configure(States.q1).Permit(Triggers.CapitalLetters, States.q2);
            stateMachine.Configure(States.q2).Permit(Triggers.Dash, States.q1);
            stateMachine.Configure(States.q2).Permit(Triggers.Space, States.q3);
            stateMachine.Configure(States.q3).Permit(Triggers.CapitalLetters, States.q4);
            stateMachine.Configure(States.q4).Permit(Triggers.Dot, States.q5);
            stateMachine.Configure(States.q5).Permit(Triggers.CapitalLetters, States.q6);
            stateMachine.Configure(States.q6).Permit(Triggers.Dot, States.q7);
            stateMachine.Configure(States.q6).Permit(Triggers.Space, States.q7);
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
        }
        public void Dot()
        {
            if (stateMachine.CanFire(Triggers.Dot))
                stateMachine.Fire(Triggers.Dot);
        }
        public void Dash()
        {
            if (stateMachine.CanFire(Triggers.Dash))
                stateMachine.Fire(Triggers.Dash);
        }
        public States getState()
        {
            var q = stateMachine.State;
            return q;
        }
        public string getStr() => str;
        public bool SymbolChecking(char symbol)
        {
            if (stateMachine.State == States.q1)
                str = "";
            int kode = (int)symbol;
            if ((kode >= 1040 && kode <= 1071) || kode == 1025)
                CapitalLetters();
            else if ((kode >= 1072 && kode <= 1103) || kode == 1105)
                LowercaseLetters();
            else if (symbol == ' ') Space();
            else if (symbol == '.') Dot();
            else if (symbol == '-') Dash();
            else
            {
                str = "";
                return false;
            }
            str += symbol;
            return true;
        }
    }
}
