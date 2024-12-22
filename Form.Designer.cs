namespace KASDLab22
{
    partial class Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.OperationType = new System.Windows.Forms.ComboBox();
            this.start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(222, 12);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(700, 426);
            this.zedGraphControl1.TabIndex = 0;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            // 
            // OperationType
            // 
            this.OperationType.FormattingEnabled = true;
            this.OperationType.Items.AddRange(new object[] {
            "put(K key, V value)",
            "get(K key)",
            "remove(K key)"});
            this.OperationType.Location = new System.Drawing.Point(27, 69);
            this.OperationType.Name = "OperationType";
            this.OperationType.Size = new System.Drawing.Size(151, 21);
            this.OperationType.TabIndex = 1;
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(27, 139);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(151, 23);
            this.start.TabIndex = 4;
            this.start.Text = "Запустить тесты";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.VisGraph);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 450);
            this.Controls.Add(this.start);
            this.Controls.Add(this.OperationType);
            this.Controls.Add(this.zedGraphControl1);
            this.Name = "Form";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.ComboBox OperationType;
        private System.Windows.Forms.Button start;
    }
}

