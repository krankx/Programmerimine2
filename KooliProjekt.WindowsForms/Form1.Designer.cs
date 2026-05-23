namespace KooliProjekt.WindowsForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();

            labelId = new Label();
            labelNimetus = new Label();
            labelEnergia = new Label();
            labelValgud = new Label();
            labelSusivesikud = new Label();
            labelMillestSuhkrud = new Label();
            labelRasvad = new Label();
            labelMillestKullastunud = new Label();
            labelKiudained = new Label();
            labelSool = new Label();

            idField = new TextBox();
            nimetusField = new TextBox();
            energiaField = new TextBox();
            valgudField = new TextBox();
            susivesikudField = new TextBox();
            millestSuhkrudField = new TextBox();
            rasvadField = new TextBox();
            millestKullastunudField = new TextBox();
            kiudainedField = new TextBox();
            soolField = new TextBox();

            saveCommand = new Button();
            addCommand = new Button();
            deleteCommand = new Button();

            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            //
            // dataGridView1
            //
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(525, 500);
            dataGridView1.TabIndex = 0;
            //
            // labels
            //
            labelId.AutoSize = true; labelId.Location = new Point(543, 18); labelId.Text = "ID:";
            labelNimetus.AutoSize = true; labelNimetus.Location = new Point(543, 47); labelNimetus.Text = "Nimetus:";
            labelEnergia.AutoSize = true; labelEnergia.Location = new Point(543, 76); labelEnergia.Text = "Energia:";
            labelValgud.AutoSize = true; labelValgud.Location = new Point(543, 105); labelValgud.Text = "Valgud:";
            labelSusivesikud.AutoSize = true; labelSusivesikud.Location = new Point(543, 134); labelSusivesikud.Text = "Susivesikud:";
            labelMillestSuhkrud.AutoSize = true; labelMillestSuhkrud.Location = new Point(543, 163); labelMillestSuhkrud.Text = "Millest suhkrud:";
            labelRasvad.AutoSize = true; labelRasvad.Location = new Point(543, 192); labelRasvad.Text = "Rasvad:";
            labelMillestKullastunud.AutoSize = true; labelMillestKullastunud.Location = new Point(543, 221); labelMillestKullastunud.Text = "Millest kullastunud:";
            labelKiudained.AutoSize = true; labelKiudained.Location = new Point(543, 250); labelKiudained.Text = "Kiudained:";
            labelSool.AutoSize = true; labelSool.Location = new Point(543, 279); labelSool.Text = "Sool:";
            //
            // text fields
            //
            idField.Location = new Point(700, 15); idField.Size = new Size(120, 23); idField.ReadOnly = true; idField.Text = "-1";
            nimetusField.Location = new Point(700, 44); nimetusField.Size = new Size(120, 23);
            energiaField.Location = new Point(700, 73); energiaField.Size = new Size(120, 23);
            valgudField.Location = new Point(700, 102); valgudField.Size = new Size(120, 23);
            susivesikudField.Location = new Point(700, 131); susivesikudField.Size = new Size(120, 23);
            millestSuhkrudField.Location = new Point(700, 160); millestSuhkrudField.Size = new Size(120, 23);
            rasvadField.Location = new Point(700, 189); rasvadField.Size = new Size(120, 23);
            millestKullastunudField.Location = new Point(700, 218); millestKullastunudField.Size = new Size(120, 23);
            kiudainedField.Location = new Point(700, 247); kiudainedField.Size = new Size(120, 23);
            soolField.Location = new Point(700, 276); soolField.Size = new Size(120, 23);
            //
            // buttons
            //
            saveCommand.Location = new Point(543, 320);
            saveCommand.Name = "saveCommand";
            saveCommand.Size = new Size(85, 28);
            saveCommand.Text = "Salvesta";
            saveCommand.UseVisualStyleBackColor = true;

            addCommand.Location = new Point(634, 320);
            addCommand.Name = "addCommand";
            addCommand.Size = new Size(85, 28);
            addCommand.Text = "Lisa uus";
            addCommand.UseVisualStyleBackColor = true;

            deleteCommand.Location = new Point(725, 320);
            deleteCommand.Name = "deleteCommand";
            deleteCommand.Size = new Size(85, 28);
            deleteCommand.Text = "Kustuta";
            deleteCommand.UseVisualStyleBackColor = true;
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 520);

            Controls.Add(deleteCommand);
            Controls.Add(addCommand);
            Controls.Add(saveCommand);

            Controls.Add(idField);
            Controls.Add(nimetusField);
            Controls.Add(energiaField);
            Controls.Add(valgudField);
            Controls.Add(susivesikudField);
            Controls.Add(millestSuhkrudField);
            Controls.Add(rasvadField);
            Controls.Add(millestKullastunudField);
            Controls.Add(kiudainedField);
            Controls.Add(soolField);

            Controls.Add(labelId);
            Controls.Add(labelNimetus);
            Controls.Add(labelEnergia);
            Controls.Add(labelValgud);
            Controls.Add(labelSusivesikud);
            Controls.Add(labelMillestSuhkrud);
            Controls.Add(labelRasvad);
            Controls.Add(labelMillestKullastunud);
            Controls.Add(labelKiudained);
            Controls.Add(labelSool);

            Controls.Add(dataGridView1);

            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Toiduained";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;

        private Label labelId;
        private Label labelNimetus;
        private Label labelEnergia;
        private Label labelValgud;
        private Label labelSusivesikud;
        private Label labelMillestSuhkrud;
        private Label labelRasvad;
        private Label labelMillestKullastunud;
        private Label labelKiudained;
        private Label labelSool;

        private TextBox idField;
        private TextBox nimetusField;
        private TextBox energiaField;
        private TextBox valgudField;
        private TextBox susivesikudField;
        private TextBox millestSuhkrudField;
        private TextBox rasvadField;
        private TextBox millestKullastunudField;
        private TextBox kiudainedField;
        private TextBox soolField;

        private Button saveCommand;
        private Button addCommand;
        private Button deleteCommand;
    }
}
