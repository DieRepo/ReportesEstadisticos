<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ReportesEstadisticos.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

        <%--CSS--%>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.6.3/css/all.css"  />
    <link rel="stylesheet" href="Css/login.css" />
    <link rel="stylesheet" href="PNotify/animate.css" />
    <link rel="stylesheet" href="PNotify/pnotify.custom.min.css" />

    <%--JS--%>
      <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" ></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="Scripts/Sesion.js"></script>
     <script src="PNotify/pnotify.custom.min.js"></script>
</head>
<body>
        <form runat="server" class="login_contenedor d-flex align-items-center justify-content-center">
        <asp:ScriptManager ID="ajax_login" runat="server"/>


            <asp:Panel runat="server">

                <section class="vh-100">
  <div class="container-fluid h-custom">
    <div class="row d-flex justify-content-center align-items-center h-100">
      <div class="col-md-9 col-lg-6 col-xl-5">
        <img src="https://infoestadisticapj.pjedomex.gob.mx/sistemacorreciones/img/poderJudiciallogo.jpg"
          class="img-fluid" alt="Sample image">
      </div>
      <div class="col-md-8 col-lg-6 col-xl-4 offset-xl-1">
          <div class="d-flex flex-row align-items-center justify-content-center justify-content-lg-start">
            <p class="lead fw-normal mb-0 me-3">SISTEMA DE REPORTES ESTADISTICOS</p>
            <%--<button type="button" class="btn btn-primary btn-floating mx-1">
              <i class="fab fa-facebook-f"></i>
            </button>

            <button type="button" class="btn btn-primary btn-floating mx-1">
              <i class="fab fa-twitter"></i>
            </button>

            <button type="button" class="btn btn-primary btn-floating mx-1">
              <i class="fab fa-linkedin-in"></i>
            </button>--%>
          </div>

          <div class="divider d-flex align-items-center my-4">
            <p class="text-center fw-bold mx-3 mb-0">DIE</p>
          </div>

          <!-- Email input -->
          <div class="form-outline mb-4">
            <asp:TextBox ID="TextBox_user_name" runat="server" class="form-control form-control-lg" Placeholder="Ingrese su usuario"/>
            <label class="form-label" for="form3Example3">Usuario</label>
          </div>

            <!-- Password input -->
            <div class="form-outline mb-3">
                <asp:TextBox TextMode="Password" ID="TextBox_password" runat="server" class="form-control form-control-lg" Placeholder="Ingrese su contraseña" />

                <label class="form-label" for="form3Example4">Contraseña</label>
            </div>

          <div class="d-flex justify-content-between align-items-center">
            <!-- Checkbox -->
            <div class="form-check mb-0">
              <input class="form-check-input me-2" type="checkbox" value="" id="form2Example3" />
              <label class="form-check-label" for="form2Example3">
                Remember me
              </label>
            </div>
<%--            <a href="#!" class="text-body">Forgot password?</a>--%>
          </div>

                    <div class="text-center text-lg-start mt-4 pt-2">
            <asp:Button runat="server" type="button" class="btn btn-primary btn-lg" OnClick="btn_login_Click" Text="Ingresar" Cssclass="btn btn-dark"
              style="padding-left: 2.5rem; padding-right: 2.5rem;"></asp:Button>

          </div>



      </div>
    </div>
  </div>
  <div
    class="d-flex flex-column flex-md-row text-center text-md-start justify-content-between py-4 px-4 px-xl-5 bg-primary">
    <!-- Copyright -->
    <div class="text-white mb-3 mb-md-0">
      PODER JUDICIAL DEL ESTADO DE MÉXICO
    </div>
    <!-- Copyright -->

    <!-- Right -->
    <div>
      <a href="#!" class="text-white me-4">
        <i class="fab fa-facebook-f"></i>
      </a>
      <a href="#!" class="text-white me-4">
        <i class="fab fa-twitter"></i>
      </a>
      <a href="#!" class="text-white me-4">
        <i class="fab fa-google"></i>
      </a>
      <a href="#!" class="text-white">
        <i class="fab fa-linkedin-in"></i>
      </a>
    </div>
    <!-- Right -->
  </div>
</section>


            </asp:Panel>
    </form>
</body>
</html>
