<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormatoAnticorrupcion.aspx.cs" Inherits="ReportesEstadisticos.FormatoAnticorrupcion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel runat="server" >

        <h1>FORMATO PARA LA ENTREGA DE INFORMACIÓN QUINTO INFORME ANUAL DEL CC    </h1>
        <br />
        <h2>V. INFORME DETALLADO DE SENTENCIAS EJECUTORIAS SOBRE DELITOS EN MATERIA DE CORRUPCIÓN    </h2>
        <br />

        <asp:Panel runat="server" class="card">
            <asp:Panel runat="server" class="card-header">
                Fundamento jurídico
            </asp:Panel>
            <div class="card-body">
                <%--    <h5 class="card-title">Special title treatment</h5>--%>
                <p class="card-text">Ley del Sistema Anticorrupción del Estado de México y Municipios (LSAEMM), Artículo 9 fracción VIII. Emitir un informe anual que contenga los avances y resultados del ejercicio de sus funciones y de la aplicación de políticas y programas estatales y municipales en la materia.</p>
            </div>
        </asp:Panel>

        <br />
        <asp:Panel runat="server" class="card">
            <asp:Panel runat="server" class="card-header">
                Datos generales
            </asp:Panel>
            <div class="card-body">
                <%--    <h5 class="card-title">Special title treatment</h5>--%>
                <p class="card-text">
                    <b>Ente público:  </b>Poder Judicial del Estado de México
                </p>

                <p>
                    <asp:Label runat="server" Text="Nombre y cargo de la persona que proporciona la información:" Font-Bold="true"></asp:Label>
                    <asp:Label runat="server" Text="Lic. M. A. C. Adriana Cruz Anaya Titular de la Dirección de Información y Estadística."></asp:Label>
                </p>
                <p>
                    <asp:Label runat="server" Text="Unidad administrativa de adscripción: " Font-Bold="true"></asp:Label>
                    <asp:Label runat="server" Text="Dirección de información y Estadística. "></asp:Label>
                </p>
                <p>
                    <asp:Label runat="server" Text="Correo electrónico:" Font-Bold="true"></asp:Label>
                    <asp:Label runat="server" Text="adriana.cruz@pjedomex.gob.mx"></asp:Label>
                </p>
                <p>
                    <asp:Label runat="server" Text="Número telefónico:" Font-Bold="true"></asp:Label>
                    <asp:Label runat="server" Text=" 72271679200 Ext. 15444, 15442, 15444"></asp:Label>
                </p>

            </div>
        </asp:Panel>

        <br />
        <asp:Panel runat="server" class="card">
            <asp:Panel runat="server" class="card-header">
                Periodo del Informe Anual del CC 2021-2022
            </asp:Panel>
            <div class="card-body">
                <div class="form-group">
                    <asp:TextBox runat="server" ID="textoPeriodoInforme" CssClass="form-control" Width="100%" Rows="5"
                        TextMode="multiline">La información que proporcione en este formato comprenderá el periodo del 14 de agosto de 2021 al 12 de agosto de 2022; considerando dos cortes, el primer corte del 14 de agosto de 2021 al 31 de julio de 2022 y el segundo con actualización al 12 de agosto de 2022.
                    </asp:TextBox>
                </div>
            </div>
        </asp:Panel>


    </asp:Panel>

    <br />
    <br />
    <div class="input-group">
        <span class="input-group-text">Seleccione fechas del reporte:</span>
        <asp:TextBox runat="server" ID="fechaInicio" type="Date" required="true" aria-label="First name" class="form-control" />
        <asp:Label runat="server" ID="textAl" Text=" al " Style="margin-left: 10px; margin-right: 10px; padding-top: 10px;"></asp:Label>
        <asp:TextBox runat="server" ID="fechaFin" type="Date" required="true" aria-label="Last name" class="form-control" />
        <asp:Button ID="archivo" runat="server" OnClick="archivo_Click" Text="Generar Archivo" CssClass="btn btn-danger" />

    </div>

    <div>
    </div>
</asp:Content>
