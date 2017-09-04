/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 14/08/2017
 * Hora: 19:39
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
using Microsoft.Win32;

namespace ComparadorDeArchivos
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		byte[] archivoLeft;
		byte[] archivoRight;
		
		public Window1()
		{
			InitializeComponent();
		}
		void MiLeft_Click(object sender, RoutedEventArgs e)
		{
			archivoLeft=DameArchivoBytes();
		}
		void MiRigth_Click(object sender, RoutedEventArgs e)
		{
			archivoRight=DameArchivoBytes();
		}

		byte[] DameArchivoBytes()
		{
			byte[] archivo;
			OpenFileDialog opn=new OpenFileDialog();
			if(opn.ShowDialog().GetValueOrDefault())
			{
				archivo=System.IO.File.ReadAllBytes(opn.FileName);
			}
			else archivo=null;
			return archivo;
		}

		void MiCompara_Click(object sender, RoutedEventArgs e)
		{
			const int NOENCONTRADO=-1;
			List<Diferencia> diferencias=new List<Diferencia>();
			int offsetDiferenteInicio=NOENCONTRADO;
			if(archivoLeft!=null&&archivoRight!=null)
			{
				stkDiferences.Children.Clear();
				//comparo
				unsafe{
					byte* ptrBytesL;
					byte* ptrBytesR;
					fixed(byte* ptBytesL=archivoLeft){
						fixed(byte* ptBytesR=archivoRight)
						{
							ptrBytesL=ptBytesL;
							ptrBytesR=ptBytesR;
							for(int i=0,f=archivoLeft.Length<archivoRight.Length?archivoLeft.Length:archivoRight.Length;i<f;i++)
							{
								//cuando encuentro una diferencia guardo el offset hasta que acaben las diferencias y luego cojo los bytes diferentes de ambos y luego continuo
								if(offsetDiferenteInicio!=NOENCONTRADO)
								{
									if(*ptrBytesL==*ptrBytesR)
									{
										//se acaba la diferencia
										diferencias.Add(new Diferencia(offsetDiferenteInicio,i-offsetDiferenteInicio,archivoLeft,archivoRight));
										offsetDiferenteInicio=NOENCONTRADO;
									}
									
								}else if(*ptrBytesL!=*ptrBytesR){
									offsetDiferenteInicio=i;
								}
								ptrBytesL++;
								ptrBytesR++;
							}
						}
					}
					
				}
				for(int i=0;i<diferencias.Count;i++)
					stkDiferences.Children.Add(diferencias[i]);
				MessageBox.Show("Se ha encontrado "+stkDiferences.Children.Count+" diferencias");
			}
			else MessageBox.Show("Falta cargar:  Left("+(archivoLeft==null?"SI":"NO")+") Right("+(archivoRight==null?"SI":"NO")+")","Atención, si es que SI es que falta por cargar");
		}
	}
}