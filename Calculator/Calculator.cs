using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using DynamicExpresso;

namespace Calculator
{
    [ApiVersion(1, 23)]
    public class Calculator : TerrariaPlugin
    {
        #region Info
        public override string Name { get { return "Calculator"; } }
        public override string Author { get { return "Ryozuki"; } }
        public override string Description { get { return "A calculator"; } }
        public override Version Version { get { return new Version(1, 0, 0); } }
        #endregion

        public Calculator(Main game) : base(game)
        {

        }

        #region Initialize
        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
            }
        }

        void OnInitialize(EventArgs args)
        {
            Commands.ChatCommands.Add(new Command("calculator.use", calculator, "calc")
            {
                HelpText = "Usage: /calc <operation>"
            });
        }

        void calculator(CommandArgs e)
        {
            if (e.Parameters.Count == 0)
            {
                e.Player.SendErrorMessage("Invalid syntax! Proper syntax: {0}calculator <operation>", Commands.Specifier);
                return;
            }

            string calcArgs = String.Join(" ", e.Parameters.ToArray());
            var interpreter = new Interpreter();

            try
            {
                var result = interpreter.Eval(calcArgs);
                e.Player.SendSuccessMessage("Result is: {0}.", result);
            }
            catch
            {
                e.Player.SendErrorMessage("Invalid syntax! Proper syntax: {0}calc <operation>", Commands.Specifier);
            }
        }
    }
}
