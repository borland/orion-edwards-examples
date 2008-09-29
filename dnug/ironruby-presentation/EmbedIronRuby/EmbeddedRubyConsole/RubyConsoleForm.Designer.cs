namespace EmbeddedRubyConsole
{
	partial class RubyConsoleForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.code = new System.Windows.Forms.RichTextBox();
			this.output = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// code
			// 
			this.code.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.code.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.code.Location = new System.Drawing.Point(0, 356);
			this.code.Name = "code";
			this.code.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.code.Size = new System.Drawing.Size(739, 73);
			this.code.TabIndex = 0;
			this.code.Text = "";
			this.code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.code_KeyDown);
			// 
			// output
			// 
			this.output.BackColor = System.Drawing.SystemColors.Info;
			this.output.Dock = System.Windows.Forms.DockStyle.Fill;
			this.output.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.output.Location = new System.Drawing.Point(0, 0);
			this.output.Multiline = true;
			this.output.Name = "output";
			this.output.ReadOnly = true;
			this.output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.output.Size = new System.Drawing.Size(739, 356);
			this.output.TabIndex = 1;
			// 
			// RubyConsoleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(739, 429);
			this.Controls.Add(this.output);
			this.Controls.Add(this.code);
			this.Name = "RubyConsoleForm";
			this.Text = "RubyConsole";
			this.Load += new System.EventHandler(this.RubyConsoleForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox code;
		private System.Windows.Forms.TextBox output;
	}
}

