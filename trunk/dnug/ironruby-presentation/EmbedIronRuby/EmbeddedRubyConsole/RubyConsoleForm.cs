using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using IronRuby;
using System.Reflection;

namespace EmbeddedRubyConsole
{
	public partial class RubyConsoleForm : Form
	{
		public RubyConsoleForm()
		{
			InitializeComponent();

			SetupEngine();
		}

		private TextWriter writer;
		private ScriptEngine engine;
		private ScriptScope scope;

		private void SetupEngine()
		{
			var runtime = new ScriptRuntime( Ruby.CreateRuntimeSetup() );

			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				runtime.LoadAssembly(assembly); // give ruby access to all our assemblies
			}

			writer = SetupIO( runtime );
			engine = runtime.GetEngine( "IronRuby" );
			scope = engine.CreateScope();
		}

		private TextWriter SetupIO( ScriptRuntime runtime )
		{
			var writer = new TextBoxWriter( output );
			var stream = new TextWriterStream( writer );
			runtime.IO.SetOutput( stream, writer );
			runtime.IO.SetErrorOutput( stream, writer );

			return writer;
		}

		private string Prettify( object obj )
		{
			if( obj == null )
				return "nil";
			else
				return obj.ToString();
		}

		private void RunCode( string theCode )
		{
			writer.WriteLine( ">>> " + theCode );

			try
			{
				var scriptSource = engine.CreateScriptSourceFromString( "("+theCode+").inspect", SourceCodeKind.Statements);
				var result = scriptSource.Execute(scope);

				writer.WriteLine( "=> " + Prettify( result ) );
			}
			catch( Exception e )
			{
				writer.WriteLine( e.Message );
			}
		}

		private void code_KeyDown( object sender, KeyEventArgs e )
		{
			if( e.Control && e.KeyCode == Keys.Return )
			{
				RunCode(this.code.Text);
				e.SuppressKeyPress = true;
			}
		}

		private void RubyConsoleForm_Load(object sender, EventArgs e)
		{
			RunCode("include System::Windows::Forms");
			RunCode("FORMS = Application.open_forms.to_a");
		}
	}
}
