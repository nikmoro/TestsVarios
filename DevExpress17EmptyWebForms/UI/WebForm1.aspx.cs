using DevExpress.Web.Data;
using System;

namespace DevExpress17EmptyWebForms.UI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        readonly BLL.DigitosMinistracion BLLDigitoMinistracion = new BLL.DigitosMinistracion();

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
            try
            {
                var ex = new Exception();

                if (!BLLDigitoMinistracion.AgregarDigitoMisnistracion((string)e.NewValues["Codigo"], (string)e.NewValues["Descripcion"]))
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
            try
            {
                var exception = new Exception();

                if (!BLLDigitoMinistracion.ActualizarDigitoMinistracion((int)e.Keys[0], (string)e.NewValues["Codigo"], (string)e.NewValues["Descripcion"]))
                    throw exception;
            }
            catch (Exception exception)
            {
                lblCatMsgError.Text = exception.Message;
            }
        }

        protected void ASPxGridView1_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                var ex = new Exception();

                if (!BLLDigitoMinistracion.EliminarVirtualDigitoMinistracion((int)e.Keys[0]))
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                lblCatMsgError.Text = ex.Message;
            }

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
        }
    }
}