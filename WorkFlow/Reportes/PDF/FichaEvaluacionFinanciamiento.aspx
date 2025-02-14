<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PDF.Master" AutoEventWireup="true" CodeBehind="FichaEvaluacionFinanciamiento.aspx.cs" Inherits="WorkFlow.Reportes.PDF.FichaEvaluacionFinanciamiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="width: 100%;">
        <table style="width: 100%">
            <tr>
                <td style="width: 70%; text-align:center;">FICHA EVALUACIÓN OPERACIÓN DE FINANCIAMIENTO
                    <br />
                    (Comercial)
                </td>
                <td style="width: 30%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 30%">N° Operación
                            </td>
                            <td style="width: 70%">
                                <anthem:Label ID="lblNumOperacion" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">Ejecutivo
                            </td>
                            <td style="width: 70%">
                                <anthem:Label ID="lblEjecutivo" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">Analista
                            </td>
                            <td style="width: 70%">
                                <anthem:Label ID="lblAnalista" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">Fecha
                            </td>
                            <td style="width: 70%">
                                <anthem:Label ID="lblFecha" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">Valor UF
                            </td>
                            <td style="width: 70%">
                                <anthem:Label ID="lblValorUF" runat="server"></anthem:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="Contenedor">
        <div class="SubTitulo" style="text-align: center;">
            ANTECEDENTES PERSONALES
        </div>
    </div>
    <div class='SubTituloDatos'>Solicitante Principal </div>
    <div>
        <table class="ContenedorTabla">
            <tr>
                <td style='width: 10%'>Rut</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Nombre</td>
                <td style='width: 2%'>:</td>
                <td style='width: 40%' colspan="4"></td>
                <td style='width: 13%'>Residencia</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr>
            <tr>                
                <td style='width: 10%'>Nacionalidad</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Sexo</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>     
                <td style='width: 15%'>Fecha Nacimiento</td>
                <td style='width: 2%'>:</td>
                <td style='width: 13%'></td>
                <td style='width: 13%'>Edad</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr>
            <tr>
                <td style='width: 10%'>Estado Civil</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                  
            </tr>
            <tr>
               
                <td style='width: 13%'>Profesión</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>                
            </tr>       
            <tr>
                <td style='width: 10%'>Estudios</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Institución</td>
                <td style='width: 2%'>:</td>
                <td style='width: 40%' colspan="4"></td>
                <td style='width: 13%'>Título Profesional</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>      
            </tr>     
            <tr>
                <td style='width: 10%'>Ocupación</td>
                <td style='width: 2%'>:</td>
                <td style='width: 59%' colspan="7"></td>                
            </tr>       
            <tr>
                <td style='width: 10%'>Domicilio</td>
                <td style='width: 2%'>:</td>
                <td style='width: 29%' colspan="4"></td>
                <td style='width: 15%'>Comuna</td>
                <td style='width: 2%'>:</td>
                <td style='width: 13%'></td>
                <td style='width: 13%'>Ciudad</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr>       
            <tr>
                <td style='width: 10%'>Habita Lugar</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Email</td>
                <td style='width: 2%'>:</td>
                <td style='width: 40%' colspan="4"></td>
                <td style='width: 13%'>Teléfono</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr>        
            <tr>
                <td style='width: 10%'>Celular</td>
                <td style='width: 2%'>:</td>
                <td style='width: 88%' colspan="10"></td>               
            </tr>
            <tr>
                <td style='width: 100%; text-align:center; font-weight:bold;' colspan="12">
                    Antecedente Laboral
                </td>
            </tr>
            <tr>
                <td style='width: 10%'>Rut</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Empleador</td>
                <td style='width: 2%'>:</td>
                <td style='width: 40%' colspan="4"></td>
                <td style='width: 13%'>Fecha Ingreso</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr>
            <tr>
                <td style='width: 10%'>Años Empresa</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Cargo</td>
                <td style='width: 2%'>:</td>
                <td style='width: 40%' colspan="4"></td>
                <td style='width: 13%'>Giro</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr>
            <tr>
                <td style='width: 10%'>Dirección</td>
                <td style='width: 2%'>:</td>
                <td style='width: 29%' colspan="4"></td>
                <td style='width: 15%'>Comuna</td>
                <td style='width: 2%'>:</td>
                <td style='width: 13%'></td>
                <td style='width: 13%'>Ciudad</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr> 
            <tr>
                <td style='width: 10%'>Celular</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Email</td>
                <td style='width: 2%'>:</td>
                <td style='width: 40%' colspan="4"></td>
                <td style='width: 13%'>Teléfono</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr>            
        </table>
    </div>
    <br />
    <div class='SubTituloDatos'>Solicitante Complementario </div>
    <div>
        <table class="ContenedorTabla">
            <tr>
                <td style='width: 10%'>Rut</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Nombre</td>
                <td style='width: 2%'>:</td>
                <td style='width: 40%' colspan="4"></td>
                <td style='width: 13%'>Residencia</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr>
            <tr>                
                <td style='width: 10%'>Nacionalidad</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Sexo</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>     
                <td style='width: 15%'>Fecha Nacimiento</td>
                <td style='width: 2%'>:</td>
                <td style='width: 13%'></td>
                <td style='width: 13%'>Edad</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr>
            <tr>
                <td style='width: 10%'>Estado Civil</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Hijos</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>     
                <td style='width: 15%'>Cargas Familiar</td>
                <td style='width: 2%'>:</td>
                <td style='width: 13%'></td>
                <td style='width: 13%'>Grupo Familiar</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>                
            </tr>
            <tr>
                <td style='width: 10%'>Rut Conyugue</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Conyugue</td>
                <td style='width: 2%'>:</td>
                <td style='width: 40%' colspan="4"></td>
                <td style='width: 13%'>Profesión</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>                
            </tr>       
            <tr>
                <td style='width: 10%'>Estudios</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Institución</td>
                <td style='width: 2%'>:</td>
                <td style='width: 40%' colspan="4"></td>
                <td style='width: 13%'>Título Profesional</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>      
            </tr>     
            <tr>
                <td style='width: 10%'>Ocupación</td>
                <td style='width: 2%'>:</td>
                <td style='width: 59%' colspan="7"></td>
                <td style='width: 13%'>Compl. Renta</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr>       
            <tr>
                <td style='width: 10%'>Domicilio</td>
                <td style='width: 2%'>:</td>
                <td style='width: 29%' colspan="4"></td>
                <td style='width: 15%'>Comuna</td>
                <td style='width: 2%'>:</td>
                <td style='width: 13%'></td>
                <td style='width: 13%'>Ciudad</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr>       
            <tr>
                <td style='width: 10%'>Habita Lugar</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Email</td>
                <td style='width: 2%'>:</td>
                <td style='width: 40%' colspan="4"></td>
                <td style='width: 13%'>Teléfono</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr>        
            <tr>
                <td style='width: 10%'>Celular</td>
                <td style='width: 2%'>:</td>
                <td style='width: 88%' colspan="10"></td>               
            </tr>
            <tr>
                <td style='width: 100%; text-align:center; font-weight:bold;' colspan="12">
                    Antecedente Laboral
                </td>
            </tr>
            <tr>
                <td style='width: 10%'>Rut</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Empleador</td>
                <td style='width: 2%'>:</td>
                <td style='width: 40%' colspan="4"></td>
                <td style='width: 13%'>Fecha Ingreso</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr>
            <tr>
                <td style='width: 10%'>Años Empresa</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Cargo</td>
                <td style='width: 2%'>:</td>
                <td style='width: 40%' colspan="4"></td>
                <td style='width: 13%'>Giro</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr>
            <tr>
                <td style='width: 10%'>Dirección</td>
                <td style='width: 2%'>:</td>
                <td style='width: 29%' colspan="4"></td>
                <td style='width: 15%'>Comuna</td>
                <td style='width: 2%'>:</td>
                <td style='width: 13%'></td>
                <td style='width: 13%'>Ciudad</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr> 
            <tr>
                <td style='width: 10%'>Celular</td>
                <td style='width: 2%'>:</td>
                <td style='width: 10%'></td>
                <td style='width: 7%'>Email</td>
                <td style='width: 2%'>:</td>
                <td style='width: 40%' colspan="4"></td>
                <td style='width: 13%'>Teléfono</td>
                <td style='width: 2%'>:</td>
                <td style='width: 14%'></td>
            </tr> 
        </table>
    </div>
    <br />
    <div class="Contenedor">
        <div class="SubTitulo" style="text-align: center;">
            INGRESOS, ESTADO SITUACION VALORES EN PESOS
        </div>
    </div>

</asp:Content>
