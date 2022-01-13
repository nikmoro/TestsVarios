using DevExpress.Web.Data;
using System;

namespace DevExpress17EmptyWebForms.UI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {   }

        /// <summary>
        /// Define los valores iniciales o por defecto al insertar un nuevo registro. https://www.youtube.com/watch?v=6EVg2V7ZB5Q
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Evento</param>
        protected void ASPxGridView1_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {   }

        protected void ASPxGridView1_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            var SubSubFuncion = new BLL.DigitosMinistracion();

            try
            {
                var ex = new Exception();

                if (!SubSubFuncion.AgregarDigitoMisnistracion((string)e.NewValues["Codigo"], (string)e.NewValues["Descripcion"]))
                    throw ex;
            }
            catch (Exception ex)
            {
                lblCatMsgError.Text = ex.Message;
            }

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
        }

        protected void ASPxGridView1_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {

        }

        protected void ASPxGridView1_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {

        }
    }
}