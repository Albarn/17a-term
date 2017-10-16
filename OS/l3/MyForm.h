#pragma once
#include <Windows.h>

namespace l3 {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for MyForm
	/// </summary>
	public ref class MyForm : public System::Windows::Forms::Form
	{
	public:
		MyForm(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~MyForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::OpenFileDialog^  openFileDialog1;
	protected:
	private: System::Windows::Forms::Button^  button1;
	private: System::Windows::Forms::Label^  label1;
	private: System::Windows::Forms::Label^  label2;
	private: System::Windows::Forms::GroupBox^  groupBox1;


	private: System::Windows::Forms::GroupBox^  groupBox2;



	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// инициализация компонента
		/// выполняется создание элементов управления,
		/// а также выводится информация о диске D:\
		/// </summary>
		void InitializeComponent(void)
		{
			this->openFileDialog1 = (gcnew System::Windows::Forms::OpenFileDialog());
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->groupBox1 = (gcnew System::Windows::Forms::GroupBox());
			this->groupBox2 = (gcnew System::Windows::Forms::GroupBox());
			this->groupBox1->SuspendLayout();
			this->groupBox2->SuspendLayout();
			this->SuspendLayout();
			// 
			// openFileDialog1
			// 
			this->openFileDialog1->FileName = L"openFileDialog1";
			this->openFileDialog1->InitialDirectory = L"D:\\";
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(6, 19);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(88, 23);
			this->button1->TabIndex = 0;
			this->button1->Text = L"выбрать файл";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &MyForm::button1_Click);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(6, 16);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(35, 13);
			this->label1->TabIndex = 1;
			this->label1->Text = L"label1";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(6, 45);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(35, 13);
			this->label2->TabIndex = 2;
			this->label2->Text = L"label2";
			// 
			// groupBox1
			// 
			this->groupBox1->Controls->Add(this->button1);
			this->groupBox1->Controls->Add(this->label2);
			this->groupBox1->Dock = System::Windows::Forms::DockStyle::Left;
			this->groupBox1->Location = System::Drawing::Point(0, 0);
			this->groupBox1->Name = L"groupBox1";
			this->groupBox1->Size = System::Drawing::Size(140, 261);
			this->groupBox1->TabIndex = 3;
			this->groupBox1->TabStop = false;
			this->groupBox1->Text = L"файл";
			// 
			// groupBox2
			// 
			this->groupBox2->Controls->Add(this->label1);
			this->groupBox2->Dock = System::Windows::Forms::DockStyle::Fill;
			this->groupBox2->Location = System::Drawing::Point(140, 0);
			this->groupBox2->Name = L"groupBox2";
			this->groupBox2->Size = System::Drawing::Size(144, 261);
			this->groupBox2->TabIndex = 4;
			this->groupBox2->TabStop = false;
			this->groupBox2->Text = L"система";
			// 
			// MyForm
			// 
			this->Font = (gcnew System::Drawing::Font(L"Courier New", 9));
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(284, 261);
			this->Controls->Add(this->groupBox2);
			this->Controls->Add(this->groupBox1);
			this->Name = L"MyForm";
			this->Text = L"OS: L3";
			this->groupBox1->ResumeLayout(false);
			this->groupBox1->PerformLayout();
			this->groupBox2->ResumeLayout(false);
			this->groupBox2->PerformLayout();
			this->ResumeLayout(false);

			//параметры для метода получения информации о диске
			WCHAR NameBuffer[MAX_PATH];
			WCHAR SysNameBuffer[MAX_PATH];
			DWORD VSNumber;
			DWORD MCLength;
			DWORD FileSF;
			GetVolumeInformation(L"D:\\", NameBuffer, sizeof(NameBuffer),
				&VSNumber, &MCLength, &FileSF, SysNameBuffer, sizeof(SysNameBuffer));

			//выводим максимальную длину имени
			//это константа MAX_PATH
			label1->Text = "";
			label1->Text += "длина имени: ";
			label1->Text += MAX_PATH;

			//проверяем состояние флагов
			//и выводим их имена, если они установлены
			label1->Text += "\nфлаги: ";
			if (FileSF&FILE_CASE_PRESERVED_NAMES)
				label1->Text += "\nCASE_PRESERVED_NAMES";
			if (FileSF&FILE_CASE_SENSITIVE_SEARCH)
				label1->Text += "\nCASE_SENSITIVE_SEARCH";
			if (FileSF&FILE_FILE_COMPRESSION)
				label1->Text += "\nCOMPRESSION";
			if (FileSF&FILE_NAMED_STREAMS)
				label1->Text += "\nNAMED_STREAMS";
			if (FileSF&FILE_PERSISTENT_ACLS)
				label1->Text += "\nPERSISTENT_ACLS";
			if (FileSF&FILE_READ_ONLY_VOLUME)
				label1->Text += "\nREAD_ONLY_VOLUME";
			if (FileSF&FILE_SEQUENTIAL_WRITE_ONCE)
				label1->Text += "\nSEQUENTIAL_WRITE_ONCE";
			if (FileSF&FILE_SUPPORTS_ENCRYPTION)
				label1->Text += "\nSUPPORTS_ENCRYPTION";
			if (FileSF&FILE_SUPPORTS_EXTENDED_ATTRIBUTES)
				label1->Text += "\nSUPPORTS_EXTENDED_ATTRIBUTES";
			if (FileSF&FILE_SUPPORTS_HARD_LINKS)
				label1->Text += "\nSUPPORTS_HARD_LINKS";
			if (FileSF&FILE_SUPPORTS_OBJECT_IDS)
				label1->Text += "\nSUPPORTS_OBJECT_IDS";
			if (FileSF&FILE_SUPPORTS_OPEN_BY_FILE_ID)
				label1->Text += "\nSUPPORTS_OPEN_BY_FILE_ID";
			if (FileSF&FILE_SUPPORTS_REPARSE_POINTS)
				label1->Text += "\nSUPPORTS_REPARSE_POINTS";
			if (FileSF&FILE_SUPPORTS_SPARSE_FILES)
				label1->Text += "\nSUPPORTS_SPARSE_FILES";
			if (FileSF&FILE_SUPPORTS_TRANSACTIONS)
				label1->Text += "\nSUPPORTS_TRANSACTIONS";
			if (FileSF&FILE_SUPPORTS_USN_JOURNAL)
				label1->Text += "\nSUPPORTS_USN_JOURNAL";
			if (FileSF&FILE_UNICODE_ON_DISK)
				label1->Text += "\nUNICODE_ON_DISK";
			if (FileSF&FILE_VOLUME_IS_COMPRESSED)
				label1->Text += "\nVOLUME_IS_COMPRESSED";
			if (FileSF&FILE_VOLUME_QUOTAS)
				label1->Text += "\nVOLUME_QUOTAS";
		}
#pragma endregion

		//обработчик кнопки "выбрать файл"
		//здесь открывается диалоговое окно выбора файла
		//а также вывод соответствующей информации
	private: System::Void button1_Click(System::Object^  sender, System::EventArgs^  e) {
		//выбираем файл
		openFileDialog1->ShowDialog();
		WCHAR path[MAX_PATH * 10];
		int i = 0, len = openFileDialog1->FileName->Length;
		for (; i < len; i++)
			path[i] = (WCHAR)openFileDialog1->FileName[i];
		path[i] = '\0';
		HANDLE hFile = CreateFile(
			path,
			GENERIC_READ,
			FILE_SHARE_READ,
			NULL,
			OPEN_EXISTING,
			FILE_ATTRIBUTE_NORMAL,
			NULL);

		//получаем информацию о файле
		BY_HANDLE_FILE_INFORMATION info;
		GetFileInformationByHandle(hFile, &info);

		//выводим тип
		label2->Text = "тип файла: ";
		DWORD fType = GetFileType(hFile);
		switch (fType) {
			case FILE_TYPE_CHAR: label2->Text += "CHAR"; break;
			case FILE_TYPE_DISK: label2->Text += "DISK"; break;
			case FILE_TYPE_PIPE: label2->Text += "PIPE"; break;
			case FILE_TYPE_REMOTE: label2->Text += "REMOTE"; break;
			case FILE_TYPE_UNKNOWN: label2->Text += "UNKNOWN"; break;
		}

		//размер
		label2->Text += "\nразмер: ";
		DWORD fSizeHigh;
		label2->Text += GetFileSize(hFile, &fSizeHigh);

		//атрибут
		label2->Text += "\nатрибут: ";
		switch ((&info)->dwFileAttributes) {
			case FILE_ATTRIBUTE_ARCHIVE: label2->Text += "\nARCHIVE"; break;
			case FILE_ATTRIBUTE_COMPRESSED: label2->Text += "\nCOMPRESSED"; break;
			case FILE_ATTRIBUTE_DEVICE: label2->Text += "\nDEVICE"; break;
			case FILE_ATTRIBUTE_DIRECTORY: label2->Text += "\nDIRECTORY"; break;
			case FILE_ATTRIBUTE_ENCRYPTED: label2->Text += "\nENCRYPTED"; break;
			case FILE_ATTRIBUTE_HIDDEN: label2->Text += "\nHIDDEN"; break;
			case FILE_ATTRIBUTE_INTEGRITY_STREAM: label2->Text += "\nINTEGRITY_STREAM"; break;
			case FILE_ATTRIBUTE_NORMAL: label2->Text += "\nNORMAL"; break;
			case FILE_ATTRIBUTE_NOT_CONTENT_INDEXED: label2->Text += "\nNOT_CONTENT_INDEXED"; break;
			case FILE_ATTRIBUTE_NO_SCRUB_DATA: label2->Text += "\nNO_SCRUB_DATA"; break;
			case FILE_ATTRIBUTE_OFFLINE: label2->Text += "\nOFFLINE"; break;
			case FILE_ATTRIBUTE_READONLY: label2->Text += "\nREADONLY"; break;
			case FILE_ATTRIBUTE_REPARSE_POINT: label2->Text += "\nREPARSE_POINT"; break;
			case FILE_ATTRIBUTE_SPARSE_FILE: label2->Text += "\nSPARSE_FILE"; break;
			case FILE_ATTRIBUTE_SYSTEM: label2->Text += "\nSYSTEM"; break;
			case FILE_ATTRIBUTE_TEMPORARY: label2->Text += "\nTEMPORARY"; break;
			case FILE_ATTRIBUTE_VIRTUAL: label2->Text += "\nVIRTUAL"; break;
		}
	}
};
}
