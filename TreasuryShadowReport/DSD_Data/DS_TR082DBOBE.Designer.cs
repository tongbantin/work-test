﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
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
    [global::System.Xml.Serialization.XmlRootAttribute("DS_TR082DBOBE")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class DS_TR082DBOBE : global::System.Data.DataSet {
        
        private TB_TR082DBOBEDataTable tableTB_TR082DBOBE;
        
        private global::System.Data.SchemaSerializationMode _schemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public DS_TR082DBOBE() {
            this.BeginInit();
            this.InitClass();
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected DS_TR082DBOBE(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
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
                if ((ds.Tables["TB_TR082DBOBE"] != null)) {
                    base.Tables.Add(new TB_TR082DBOBEDataTable(ds.Tables["TB_TR082DBOBE"]));
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
        public TB_TR082DBOBEDataTable TB_TR082DBOBE {
            get {
                return this.tableTB_TR082DBOBE;
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
            DS_TR082DBOBE cln = ((DS_TR082DBOBE)(base.Clone()));
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
                if ((ds.Tables["TB_TR082DBOBE"] != null)) {
                    base.Tables.Add(new TB_TR082DBOBEDataTable(ds.Tables["TB_TR082DBOBE"]));
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
            this.tableTB_TR082DBOBE = ((TB_TR082DBOBEDataTable)(base.Tables["TB_TR082DBOBE"]));
            if ((initTable == true)) {
                if ((this.tableTB_TR082DBOBE != null)) {
                    this.tableTB_TR082DBOBE.InitVars();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "DS_TR082DBOBE";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/DS_TR082DBOBE.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableTB_TR082DBOBE = new TB_TR082DBOBEDataTable();
            base.Tables.Add(this.tableTB_TR082DBOBE);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeTB_TR082DBOBE() {
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
            DS_TR082DBOBE ds = new DS_TR082DBOBE();
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
        
        public delegate void TB_TR082DBOBERowChangeEventHandler(object sender, TB_TR082DBOBERowChangeEvent e);
        
        /// <summary>
        ///Represents the strongly named DataTable class.
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [global::System.Serializable()]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class TB_TR082DBOBEDataTable : global::System.Data.TypedTableBase<TB_TR082DBOBERow> {
            
            private global::System.Data.DataColumn columnDEALNO;
            
            private global::System.Data.DataColumn columnISSUER_NAME;
            
            private global::System.Data.DataColumn columnISSUE_DATE;
            
            private global::System.Data.DataColumn columnMDATE;
            
            private global::System.Data.DataColumn columnPAYER_NAME;
            
            private global::System.Data.DataColumn columnAMOUNT;
            
            private global::System.Data.DataColumn columnCOUPON;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TB_TR082DBOBEDataTable() {
                this.TableName = "TB_TR082DBOBE";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal TB_TR082DBOBEDataTable(global::System.Data.DataTable table) {
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
            protected TB_TR082DBOBEDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn DEALNOColumn {
                get {
                    return this.columnDEALNO;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn ISSUER_NAMEColumn {
                get {
                    return this.columnISSUER_NAME;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn ISSUE_DATEColumn {
                get {
                    return this.columnISSUE_DATE;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn MDATEColumn {
                get {
                    return this.columnMDATE;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn PAYER_NAMEColumn {
                get {
                    return this.columnPAYER_NAME;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn AMOUNTColumn {
                get {
                    return this.columnAMOUNT;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn COUPONColumn {
                get {
                    return this.columnCOUPON;
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
            public TB_TR082DBOBERow this[int index] {
                get {
                    return ((TB_TR082DBOBERow)(this.Rows[index]));
                }
            }
            
            public event TB_TR082DBOBERowChangeEventHandler TB_TR082DBOBERowChanging;
            
            public event TB_TR082DBOBERowChangeEventHandler TB_TR082DBOBERowChanged;
            
            public event TB_TR082DBOBERowChangeEventHandler TB_TR082DBOBERowDeleting;
            
            public event TB_TR082DBOBERowChangeEventHandler TB_TR082DBOBERowDeleted;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddTB_TR082DBOBERow(TB_TR082DBOBERow row) {
                this.Rows.Add(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TB_TR082DBOBERow AddTB_TR082DBOBERow(string DEALNO, string ISSUER_NAME, string ISSUE_DATE, string MDATE, string PAYER_NAME, decimal AMOUNT, decimal COUPON) {
                TB_TR082DBOBERow rowTB_TR082DBOBERow = ((TB_TR082DBOBERow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        DEALNO,
                        ISSUER_NAME,
                        ISSUE_DATE,
                        MDATE,
                        PAYER_NAME,
                        AMOUNT,
                        COUPON};
                rowTB_TR082DBOBERow.ItemArray = columnValuesArray;
                this.Rows.Add(rowTB_TR082DBOBERow);
                return rowTB_TR082DBOBERow;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override global::System.Data.DataTable Clone() {
                TB_TR082DBOBEDataTable cln = ((TB_TR082DBOBEDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataTable CreateInstance() {
                return new TB_TR082DBOBEDataTable();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnDEALNO = base.Columns["DEALNO"];
                this.columnISSUER_NAME = base.Columns["ISSUER_NAME"];
                this.columnISSUE_DATE = base.Columns["ISSUE_DATE"];
                this.columnMDATE = base.Columns["MDATE"];
                this.columnPAYER_NAME = base.Columns["PAYER_NAME"];
                this.columnAMOUNT = base.Columns["AMOUNT"];
                this.columnCOUPON = base.Columns["COUPON"];
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnDEALNO = new global::System.Data.DataColumn("DEALNO", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnDEALNO);
                this.columnISSUER_NAME = new global::System.Data.DataColumn("ISSUER_NAME", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnISSUER_NAME);
                this.columnISSUE_DATE = new global::System.Data.DataColumn("ISSUE_DATE", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnISSUE_DATE);
                this.columnMDATE = new global::System.Data.DataColumn("MDATE", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnMDATE);
                this.columnPAYER_NAME = new global::System.Data.DataColumn("PAYER_NAME", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnPAYER_NAME);
                this.columnAMOUNT = new global::System.Data.DataColumn("AMOUNT", typeof(decimal), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnAMOUNT);
                this.columnCOUPON = new global::System.Data.DataColumn("COUPON", typeof(decimal), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnCOUPON);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TB_TR082DBOBERow NewTB_TR082DBOBERow() {
                return ((TB_TR082DBOBERow)(this.NewRow()));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new TB_TR082DBOBERow(builder);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Type GetRowType() {
                return typeof(TB_TR082DBOBERow);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.TB_TR082DBOBERowChanged != null)) {
                    this.TB_TR082DBOBERowChanged(this, new TB_TR082DBOBERowChangeEvent(((TB_TR082DBOBERow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.TB_TR082DBOBERowChanging != null)) {
                    this.TB_TR082DBOBERowChanging(this, new TB_TR082DBOBERowChangeEvent(((TB_TR082DBOBERow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.TB_TR082DBOBERowDeleted != null)) {
                    this.TB_TR082DBOBERowDeleted(this, new TB_TR082DBOBERowChangeEvent(((TB_TR082DBOBERow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.TB_TR082DBOBERowDeleting != null)) {
                    this.TB_TR082DBOBERowDeleting(this, new TB_TR082DBOBERowChangeEvent(((TB_TR082DBOBERow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveTB_TR082DBOBERow(TB_TR082DBOBERow row) {
                this.Rows.Remove(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                DS_TR082DBOBE ds = new DS_TR082DBOBE();
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
                attribute2.FixedValue = "TB_TR082DBOBEDataTable";
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
        public partial class TB_TR082DBOBERow : global::System.Data.DataRow {
            
            private TB_TR082DBOBEDataTable tableTB_TR082DBOBE;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal TB_TR082DBOBERow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableTB_TR082DBOBE = ((TB_TR082DBOBEDataTable)(this.Table));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string DEALNO {
                get {
                    try {
                        return ((string)(this[this.tableTB_TR082DBOBE.DEALNOColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'DEALNO\' in table \'TB_TR082DBOBE\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR082DBOBE.DEALNOColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string ISSUER_NAME {
                get {
                    try {
                        return ((string)(this[this.tableTB_TR082DBOBE.ISSUER_NAMEColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'ISSUER_NAME\' in table \'TB_TR082DBOBE\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR082DBOBE.ISSUER_NAMEColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string ISSUE_DATE {
                get {
                    try {
                        return ((string)(this[this.tableTB_TR082DBOBE.ISSUE_DATEColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'ISSUE_DATE\' in table \'TB_TR082DBOBE\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR082DBOBE.ISSUE_DATEColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string MDATE {
                get {
                    try {
                        return ((string)(this[this.tableTB_TR082DBOBE.MDATEColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'MDATE\' in table \'TB_TR082DBOBE\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR082DBOBE.MDATEColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string PAYER_NAME {
                get {
                    try {
                        return ((string)(this[this.tableTB_TR082DBOBE.PAYER_NAMEColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'PAYER_NAME\' in table \'TB_TR082DBOBE\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR082DBOBE.PAYER_NAMEColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal AMOUNT {
                get {
                    try {
                        return ((decimal)(this[this.tableTB_TR082DBOBE.AMOUNTColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'AMOUNT\' in table \'TB_TR082DBOBE\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR082DBOBE.AMOUNTColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal COUPON {
                get {
                    try {
                        return ((decimal)(this[this.tableTB_TR082DBOBE.COUPONColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'COUPON\' in table \'TB_TR082DBOBE\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTB_TR082DBOBE.COUPONColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsDEALNONull() {
                return this.IsNull(this.tableTB_TR082DBOBE.DEALNOColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetDEALNONull() {
                this[this.tableTB_TR082DBOBE.DEALNOColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsISSUER_NAMENull() {
                return this.IsNull(this.tableTB_TR082DBOBE.ISSUER_NAMEColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetISSUER_NAMENull() {
                this[this.tableTB_TR082DBOBE.ISSUER_NAMEColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsISSUE_DATENull() {
                return this.IsNull(this.tableTB_TR082DBOBE.ISSUE_DATEColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetISSUE_DATENull() {
                this[this.tableTB_TR082DBOBE.ISSUE_DATEColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsMDATENull() {
                return this.IsNull(this.tableTB_TR082DBOBE.MDATEColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetMDATENull() {
                this[this.tableTB_TR082DBOBE.MDATEColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsPAYER_NAMENull() {
                return this.IsNull(this.tableTB_TR082DBOBE.PAYER_NAMEColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetPAYER_NAMENull() {
                this[this.tableTB_TR082DBOBE.PAYER_NAMEColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsAMOUNTNull() {
                return this.IsNull(this.tableTB_TR082DBOBE.AMOUNTColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetAMOUNTNull() {
                this[this.tableTB_TR082DBOBE.AMOUNTColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsCOUPONNull() {
                return this.IsNull(this.tableTB_TR082DBOBE.COUPONColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetCOUPONNull() {
                this[this.tableTB_TR082DBOBE.COUPONColumn] = global::System.Convert.DBNull;
            }
        }
        
        /// <summary>
        ///Row event argument class
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class TB_TR082DBOBERowChangeEvent : global::System.EventArgs {
            
            private TB_TR082DBOBERow eventRow;
            
            private global::System.Data.DataRowAction eventAction;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TB_TR082DBOBERowChangeEvent(TB_TR082DBOBERow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TB_TR082DBOBERow Row {
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