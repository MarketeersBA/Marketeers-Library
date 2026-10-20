using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MarketSimulator.Reports;

public class StudyScenariosForm : Form
{
	private IContainer components = null;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label6;

	private Button button1;

	internal TextBox textBox1;

	internal TextBox textBox2;

	public StudyScenariosForm()
	{
		InitializeComponent();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (textBox1.Text != "")
		{
			base.DialogResult = DialogResult.OK;
		}
		else
		{
			MessageBox.Show(this, "You must enter name for the current sceanrio", "Missing Value", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(9, 11);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(344, 14);
		this.label1.TabIndex = 0;
		this.label1.Text = "Enter scenario description to be remarkable for a quisk access";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(20, 41);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(80, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "Scenario Name";
		this.textBox1.Location = new System.Drawing.Point(104, 38);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(218, 20);
		this.textBox1.TabIndex = 2;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(38, 66);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(60, 13);
		this.label3.TabIndex = 3;
		this.label3.Text = "Description";
		this.textBox2.Location = new System.Drawing.Point(104, 63);
		this.textBox2.Multiline = true;
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(218, 25);
		this.textBox2.TabIndex = 4;
		this.label6.BackColor = System.Drawing.Color.LightSteelBlue;
		this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.label6.Location = new System.Drawing.Point(0, 112);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(407, 13);
		this.label6.TabIndex = 12;
		this.button1.BackColor = System.Drawing.Color.Transparent;
		this.button1.Location = new System.Drawing.Point(333, 96);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(67, 25);
		this.button1.TabIndex = 13;
		this.button1.Text = "OK";
		this.button1.UseVisualStyleBackColor = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		base.AcceptButton = this.button1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ControlLightLight;
		base.ClientSize = new System.Drawing.Size(407, 125);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.textBox2);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.label6);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "StudyScenariosForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Scenario";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
