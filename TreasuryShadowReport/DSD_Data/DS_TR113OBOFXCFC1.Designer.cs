//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591

namespace KKB.Treasury.TreasuryReport.DSD_Data {
    
    
    /// <summary>
    ///Represents a strongly typed in-memory cache of data.
    ///</summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [global::System.Serializable()]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [global::System.Xml.Serialization.XmlRootAttribute("DS_TR113OBOFXCFC")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class DS_TR113OBOFXCFC : global::System.Data.DataSet {
        
        private TB_TR113OBOFXCFCDataTable tableTB_TR113OBOFXCFC;
        
        private global::System.Data.SchemaSerializationMode _schemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public DS_TR113OBOFXCFC() {
            this.BeginInit();
            this.InitClass();
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected DS_TR113OBOFXCFC(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                base(info, context, false) {
            if ((this.IsBinarySerialized(info, context) == true)) {
                this.InitVars(false);
                global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += schemaChangedHandler1;
                this.Relations.CollectionChanged += schemaChangedHandler1;
                return;
            }
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((this.DetermineSchemaSerializationMode(info, context) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
                if ((ds.Tables["TB_TR113OBOFXCFC"] != null)) {
                    base.Tables.Add(new TB_TR113OBOFXCFCDataTable(ds.Tables["TB_TR113OBOFXCFC"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
            }
            this.GetSerializationData(info, context);
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Browsable(false)]
        [global::System.ComponentModel.DesignerSerializationVisibility(global::System.ComponentModel.DesignerSerializationVisibility.Content)]
        public TB_TR113OBOFXCFCDataTable TB_TR113OBOFXCFC {
            get {
                return this.tableTB_TR113OBOFXCFC;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.BrowsableAttribute(true)]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override global::System.Data.SchemaSerializationMode SchemaSerializationMode {
            get {
                return this._schemaSerializationMode;
            }
            set {
                this._schemaSerializationMode = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataTableCollection Tables {
            get {
                return base.Tables;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataRelationCollection Relations {
            get {
                return base.Relations;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void InitializeDerivedDataSet() {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override global::System.Data.DataSet Clone() {
            DS_TR113OBOFXCFC cln = ((DS_TR113OBOFXCFC)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void ReadXmlSerializable(global::System.Xml.XmlReader reader) {
            if ((this.DetermineSchemaSerializationMode(reader) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                this.Reset();
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXml(reader);
                if ((ds.Tables["TB_TR113OBOFXCFC"] != null)) {
                    base.Tables.Add(new TB_TR113OBOFXCFCDataTable(ds.Tables["TB_TR113OBOFXCFC"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXml(reader);
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override global::System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream();
            this.WriteXmlSchema(new global::System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return global::System.Xml.Schema.XmlSchema.Read(new global::System.Xml.XmlTextReader(stream), null);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars() {
            this.InitVars(true);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars(bool initTable) {
            this.tableTB_TR113OBOFXCFC = ((TB_TR113OBOFXCFCDataTable)(base.Tables["TB_TR113OBOFXCFC"]));
            if ((initTable == true)) {
                if ((this.tableTB_TR113OBOFXCFC != null)) {
                    this.tableTB_TR113OBOFXCFC.InitVars();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "DS_TR113OBOFXCFC";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/DS_TR113OBOFXCFC.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableTB_TR113OBOFXCFC = new TB_TR113OBOFXCFCDataTable();
            base.Tables.Add(this.tableTB_TR113OBOFXCFC);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeTB_TR113OBOFXCFC() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void SchemaChanged(object sender, global::System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == global::System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
            DS_TR113OBOFXCFC ds = new DS_TR113OBOFXCFC();
            global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
            global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
            global::System.Xml.Schema.XmlSchemaAny any = new global::System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
            if (xs.Contains(dsSchema.TargetNamespace)) {
                global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                try {
                    global::System.Xml.Schema.XmlSchema schema = null;
                    dsSchema.Write(s1);
                    for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                        schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                        s2.SetLength(0);
                        schema.Write(s2);
                        if ((s1.Length == s2.Length)) {
                            s1.Position = 0;
                            s2.Position = 0;
                            for (; ((s1.Position != s1.Length) 
                                        && (s1.ReadByte() == s2.ReadByte())); ) {
                                ;
                            }
                            if ((s1.Position == s1.Length)) {
                                return type;
                            }
                        }
                    }
                }
                finally {
                    if ((s1 != null)) {
                        s1.Close();
                    }
                    if ((s2 != null)) {
                        s2.Close();
                    }
                }
            }
            xs.Add(dsSchema);
            return type;
        }
        
        public delegate void TB_TR113OBOFXCFCRowChangeEventHandler(object sender, TB_TR113OBOFXCFCRowChangeEvent e);
        
        /// <summary>
        ///Represents the strongly named DataTable class.
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [global::System.Serializable()]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class TB_TR113OBOFXCFCDataTable : global::System.Data.TypedTableBase<TB_TR113OBOFXCFCRow> {
            
            private global::System.Data.DataColumn columnBR;
            
            private global::System.Data.DataColumn columnBRANPRCDATE;
            
            private global::System.Data.DataColumn columnSN;
            
            private global::System.Data.DataColumn columnCCY;
            
            private global::System.Data.DataColumn columnCCYAMOUNT;
            
            private global::System.Data.DataColumn columnCTRCCY;
            
            private global::System.Data.DataColumn columnCTRAMOUNT;
            
            private global::System.Data.DataColumn columnSPOTRATE_8;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TB_TR113OBOFXCFCDataTable() {
                this.TableName = "TB_TR113OBOFXCFC";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal TB_TR113OBOFXCFCDataTable(global::System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected TB_TR113OBOFXCFCDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn BRColumn {
                get {
                    return this.columnBR;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn BRANPRCDATEColumn {
                get {
                    return this.columnBRANPRCDATE;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn SNColumn {
                get {
                    return this.columnSN;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn CCYColumn {
                get {
                    return this.columnCCY;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn CCYAMOUNTColumn {
                get {
                    return this.columnCCYAMOUNT;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn CTRCCYColumn {
                get {
                    return this.columnCTRCCY;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn CTRAMOUNTColumn {
                get {
                    return this.columnCTRAMOUNT;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn SPOTRATE_8Column {
                get {
                    return this.columnSPOTRATE_8;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TB_TR113OBOFXCFCRow this[int index] {
                get {
                    return ((TB_TR113OBOFXCFCRow)(this.Rows[index]));
                }
            }
            
            public event TB_TR113OBOFXCFCRowChangeEventHandler TB_TR113OBOFXCFCRowChanging;
            
            public event TB_TR113OBOFXCFCRowChangeEventHandler TB_TR113OBOFXCFCRowChanged;
            
            public event TB_TR113OBOFXCFCRowChangeEventHandler TB_TR113OBOFXCFCRowDeleting;
            
            public event TB_TR113OBOFXCFCRowChangeEventHandler TB_TR113OBOFXCFCRowDeleted;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddTB_TR113OBOFXCFCRow(TB_TR113OBOFXCFCRow row) {
                this.Rows.Add(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TB_TR113OBOFXCFCRow AddTB_TR113OBOFXCFCRow(string BR, string BRANPRCDATE, string SN, string CCY, decimal CCYAMOUNT, string CTRCCY, decimal CTRAMOUNT, decimal SPOTRATE_8) {
                TB_TR113OBOFXCFCRow rowTB_TR113OBOFXCFCRow = ((TB_TR113OBOFXCFCRow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        BR,
                        BRANPRCDATE,
                        SN,
                        CCY,
                        CCYAMOUNT,
                        CTRCCY,
                        CTRAMOUNT,
                        SPOTRATE_8};
                rowTB_TR113OBOFXCFCRow.ItemArray = columnValuesArray;
                this.Rows.Add(rowTB_TR113OBOFXCFCRow);
                return rowTB_TR113OBOFXCFCRow;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override global::System.Data.DataTable Clone() {
                TB_TR113OBOFXCFCDataTable cln = ((TB_TR113OBOFXCFCDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataTable CreateInstance() {
                return new TB_TR113OBOFXCFCDataTable();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnBR = base.Columns["BR"];
                this.columnBRANPRCDATE = base.Columns["BRANPRCDATE"];
                this.columnSN = base.Columns["SN"];
                this.columnCCY = base.Columns["CCY"];
                this.columnCCYAMOUNT = base.Columns["CCYAMOUNT"];
                this.columnCTRCCY = base.Columns["CTRCCY"];
                this.columnCTRAMOUNT = base.Columns["CTRAMOUNT"];
                this.columnSPOTRATE_8 = base.Columns["SPOTRATE_8"];
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnBR = new global::System.Data.DataColumn("BR", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnBR);
                this.columnBRANPRCDATE = new global::System.Data.DataColumn("BRANPRCDATE", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnBRANPRCDATE);
                this.columnSN = new global::System.Data.DataColumn("SN", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnSN);
                this.columnCCY = new global::System.Data.DataColumn("CCY", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnCCY);
                this.columnCCYAMOUNT = new global::System.Data.DataColumn("CCYAMOUNT", typeof(decimal), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnCCYAMOUNT);
                this.columnCTRCCY = new global::System.Data.DataColumn("CTRCCY", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnCTRCCY);
                this.columnCTRAMOUNT = new global::System.Data.DataColumn("CTRAMOUNT", typeof(decimal), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnCTRAMOUNT);
                this.columnSPOTRATE_8 = new global::System.Data.DataColumn("SPOTRATE_8", typeof(decimal), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnSPOTRATE_8);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TB_TR113OBOFXCFCRow NewTB_TR113OBOFXCFCRow() {
                return ((TB_TR113OBOFXCFCRow)(this.NewRow()));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new TB_TR113OBOFXCFCRow(builder);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Type GetRowType() {
                return typeof(TB_TR113OBOFXCFCRow);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.TB_TR113OBOFXCFCRowChanged != null)) {
                    this.TB_TR113OBOFXCFCRowChanged(this, new TB_TR113OBOFXCFCRowChangeEvent(((TB_TR113OBOFXCFCRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.TB_TR113OBOFXCFCRowChanging != null)) {
                    this.TB_TR113OBOFXCFCRowChanging(this, new TB_TR113OBOFXCFCRowChangeEvent(((TB_TR113OBOFXCFCRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.TB_TR113OBOFXCFCRowDeleted != null)) {
                    this.TB_TR113OBOFXCFCRowDeleted(this, new TB_TR113OBOFXCFCRowChangeEvent(((TB_TR113OBOFXCFCRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.TB_TR113OBOFXCFCRowDeleting != null)) {
                    this.TB_TR113OBOFXCFCRowDeleting(this, new TB_TR113OBOFXCFCRowChangeEvent(((TB_TR113OBOFXCFCRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveTB_TR113OBOFXCFCRow(TB_TR113OBOFXCFCRow row) {
                this.Rows.Remove(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                DS_TR113OBOFXCFC ds = new DS_TR113OBOFXCFC();
                global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "TB_TR113OBOFXCFCDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace)) {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length)) {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length) 
                                            && (s1.ReadByte() == s2.ReadByte())); ) {
                                    ;
                                }
                                if ((s1.Position == s1.Length)) {
                                    return type;
                                }
                            }
                        }
                    }
                    finally {
                        if ((s1 != null)) {
                            s1.Close();
                        }
                        if ((s2 != null)) {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }
        }
        
        /// <summary>
        ///Represents strongly named DataRow class.
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public partial class TB_TR113OBOFXCFCRow : global::System.Data.DataRow {
            
            private TB_TR113OBOFXCFCDataTable tableTB_TR113OBOFXCFC;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal TB_TR113OBOFXCFCRow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableTB_TR113OBOFXCFC = ((TB_TR113OBOFXCFCDataTable)(this.Table));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string BR {
                get {
                    try {
                        return ((string)(this[this.tableTB_TR113OBOFXCFC.BRColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'BR\' in table \'TB_TR113OBOFXCFC\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR113OBOFXCFC.BRColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string BRANPRCDATE {
                get {
                    try {
                        return ((string)(this[this.tableTB_TR113OBOFXCFC.BRANPRCDATEColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'BRANPRCDATE\' in table \'TB_TR113OBOFXCFC\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR113OBOFXCFC.BRANPRCDATEColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string SN {
                get {
                    try {
                        return ((string)(this[this.tableTB_TR113OBOFXCFC.SNColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'SN\' in table \'TB_TR113OBOFXCFC\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR113OBOFXCFC.SNColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string CCY {
                get {
                    try {
                        return ((string)(this[this.tableTB_TR113OBOFXCFC.CCYColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'CCY\' in table \'TB_TR113OBOFXCFC\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR113OBOFXCFC.CCYColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal CCYAMOUNT {
                get {
                    try {
                        return ((decimal)(this[this.tableTB_TR113OBOFXCFC.CCYAMOUNTColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'CCYAMOUNT\' in table \'TB_TR113OBOFXCFC\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR113OBOFXCFC.CCYAMOUNTColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string CTRCCY {
                get {
                    try {
                        return ((string)(this[this.tableTB_TR113OBOFXCFC.CTRCCYColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'CTRCCY\' in table \'TB_TR113OBOFXCFC\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR113OBOFXCFC.CTRCCYColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal CTRAMOUNT {
                get {
                    try {
                        return ((decimal)(this[this.tableTB_TR113OBOFXCFC.CTRAMOUNTColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'CTRAMOUNT\' in table \'TB_TR113OBOFXCFC\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR113OBOFXCFC.CTRAMOUNTColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal SPOTRATE_8 {
                get {
                    try {
                        return ((decimal)(this[this.tableTB_TR113OBOFXCFC.SPOTRATE_8Column]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'SPOTRATE_8\' in table \'TB_TR113OBOFXCFC\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR113OBOFXCFC.SPOTRATE_8Column] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsBRNull() {
                return this.IsNull(this.tableTB_TR113OBOFXCFC.BRColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetBRNull() {
                this[this.tableTB_TR113OBOFXCFC.BRColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsBRANPRCDATENull() {
                return this.IsNull(this.tableTB_TR113OBOFXCFC.BRANPRCDATEColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetBRANPRCDATENull() {
                this[this.tableTB_TR113OBOFXCFC.BRANPRCDATEColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSNNull() {
                return this.IsNull(this.tableTB_TR113OBOFXCFC.SNColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSNNull() {
                this[this.tableTB_TR113OBOFXCFC.SNColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsCCYNull() {
                return this.IsNull(this.tableTB_TR113OBOFXCFC.CCYColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetCCYNull() {
                this[this.tableTB_TR113OBOFXCFC.CCYColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsCCYAMOUNTNull() {
                return this.IsNull(this.tableTB_TR113OBOFXCFC.CCYAMOUNTColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetCCYAMOUNTNull() {
                this[this.tableTB_TR113OBOFXCFC.CCYAMOUNTColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsCTRCCYNull() {
                return this.IsNull(this.tableTB_TR113OBOFXCFC.CTRCCYColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetCTRCCYNull() {
                this[this.tableTB_TR113OBOFXCFC.CTRCCYColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsCTRAMOUNTNull() {
                return this.IsNull(this.tableTB_TR113OBOFXCFC.CTRAMOUNTColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetCTRAMOUNTNull() {
                this[this.tableTB_TR113OBOFXCFC.CTRAMOUNTColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSPOTRATE_8Null() {
                return this.IsNull(this.tableTB_TR113OBOFXCFC.SPOTRATE_8Column);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSPOTRATE_8Null() {
                this[this.tableTB_TR113OBOFXCFC.SPOTRATE_8Column] = global::System.Convert.DBNull;
            }
        }
        
        /// <summary>
        ///Row event argument class
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class TB_TR113OBOFXCFCRowChangeEvent : global::System.EventArgs {
            
            private TB_TR113OBOFXCFCRow eventRow;
            
            private global::System.Data.DataRowAction eventAction;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TB_TR113OBOFXCFCRowChangeEvent(TB_TR113OBOFXCFCRow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TB_TR113OBOFXCFCRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}

#pragma warning restore 1591