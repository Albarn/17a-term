#pragma once
#include <Windows.h>

namespace CppWinForm1 {

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
	private: System::Windows::Forms::ComboBox^  comboBox1;
	private: System::Windows::Forms::Label^  label1;
	protected:

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->SuspendLayout();
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(13, 39);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(35, 13);
			this->label1->TabIndex = 0;
			this->label1->Text = L"label1";
			// 
			// MyForm
			// 
			this->Font = (gcnew System::Drawing::Font(L"Courier New", 9));
			this->Text = "OS: L2";
			this->ClientSize = System::Drawing::Size(241, 169);
			this->Controls->Add(this->label1);
			this->Name = L"MyForm";
			this->ResumeLayout(false);
			this->PerformLayout();

			this->comboBox1 = (gcnew System::Windows::Forms::ComboBox());
			this->SuspendLayout();
			// 
			// comboBox1
			// 
			this->comboBox1->FormattingEnabled = true;
			this->comboBox1->Location = System::Drawing::Point(13, 13);
			this->comboBox1->Name = L"comboBox1";
			this->comboBox1->Size = System::Drawing::Size(80, 21);
			this->comboBox1->TabIndex = 0;
			this->comboBox1->SelectedIndexChanged += gcnew System::EventHandler(this, &MyForm::comboBox1_SelectedIndexChanged);
			// 
			// MyForm
			// 
			this->ClientSize = System::Drawing::Size(274, 216);
			this->Controls->Add(this->comboBox1);
			this->Name = L"MyForm";
			this->ResumeLayout(false);

			DWORD disks = GetLogicalDrives();
			for (int i = 64, j = 0; i > 0; i /= 2, j++) {
				int a = disks - i;
				if (a >= 0) {
					switch (j) {
					case 0: comboBox1->Items->Add((Object^)"A:\\"); break;
					case 1: comboBox1->Items->Add((Object^)"B:\\"); break;
					case 2: comboBox1->Items->Add((Object^)"C:\\"); break;
					case 3: comboBox1->Items->Add((Object^)"D:\\"); break;
					case 4: comboBox1->Items->Add((Object^)"E:\\"); break;
					}
					disks -= i;
				}
			}
		}
#pragma endregion
	private: System::Void comboBox1_SelectedIndexChanged(System::Object^  sender, System::EventArgs^  e) {
		WCHAR Root[MAX_PATH]; 
		WCHAR NameBuffer[MAX_PATH];
		WCHAR SysNameBuffer[MAX_PATH];
		DWORD VSNumber;
		DWORD MCLength;
		DWORD FileSF;
		for (int i = 0; i<3; i++)
			Root[i] = WCHAR(comboBox1->Text[i]);
		Root[3] = '\0';
		GetVolumeInformation(Root, NameBuffer, sizeof(NameBuffer), 
			&VSNumber, &MCLength, &FileSF, SysNameBuffer, sizeof(SysNameBuffer));
		
		label1->Text = "";
		label1->Text += "метка: ";
		for (int i = 0; Root[i] != '\0'; i++)
			label1->Text += Root[i];
		label1->Text += "\nсерийный номер:       ";
		label1->Text += VSNumber;
		label1->Text += "\nимя файловой системы: ";
		for (int i = 0; SysNameBuffer[i] != '\0'; i++)
			label1->Text += SysNameBuffer[i];
		
		DWORD SectorPC;
		DWORD BytesPS;
		DWORD FreeC;
		DWORD Clusters;
		GetDiskFreeSpace(Root, &SectorPC, &BytesPS, &FreeC, &Clusters);
		label1->Text += "\nсекторов в кластере:  ";
		label1->Text += SectorPC;
		label1->Text += "\nбайтов в секторе:     ";
		label1->Text += BytesPS;
		label1->Text += "\nразмер диска:         ";
		DWORD size = Clusters*SectorPC*BytesPS;
		label1->Text += size;
		label1->Text += "\nсвободно:             ";
		DWORD free = FreeC*SectorPC*BytesPS;
		label1->Text += free;
		label1->Text += "\nзанято:               ";
		label1->Text += size - free;
	}
	};
}
