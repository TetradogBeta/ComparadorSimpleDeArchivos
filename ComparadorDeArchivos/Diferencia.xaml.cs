/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 14/08/2017
 * Hora: 20:00
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Gabriel.Cat;
using Gabriel.Cat.Extension;
using Microsoft.Win32;
namespace ComparadorDeArchivos
{
	/// <summary>
	/// Interaction logic for Diferencia.xaml
	/// </summary>
	public partial class Diferencia : UserControl
	{
		byte[] diferenciaLeft, diferenciaRight;
		public Diferencia(int offset,int longitud,byte[] archivoLeft,byte[] archivoRight)
		{
			ContextMenu menu;
			MenuItem itSave;
			InitializeComponent();
			tbOffset.Text=(Hex)offset;
			diferenciaLeft=archivoLeft.SubArray(offset,longitud);
			tbLeft.Text=(Hex)diferenciaLeft;
			diferenciaRight=archivoRight.SubArray(offset,longitud);
			tbRight.Text=(Hex)diferenciaRight;
			
			menu=new ContextMenu();
			itSave=new MenuItem();
			itSave.Header="Save";
			itSave.Click+=(s,e)=>SaveArray(diferenciaLeft);
			menu.Items.Add(itSave);
			
			tbLeft.ContextMenu=menu;
			
			menu=new ContextMenu();
			itSave=new MenuItem();
			itSave.Header="Save";
			itSave.Click+=(s,e)=>SaveArray(diferenciaRight);
			menu.Items.Add(itSave);
			
			tbRight.ContextMenu=menu;
			
			
		}

		void SaveArray(byte[] diferencia)
		{
			SaveFileDialog sfd=new SaveFileDialog();
			sfd.DefaultExt="bin";
			if(sfd.ShowDialog().GetValueOrDefault())
				diferencia.Save(sfd.FileName);
		}
	}
}