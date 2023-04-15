using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class AnuncioFII
{
    public string id { get; set; }
    public string descricaoFundo { get; set; }
    public string categoriaDocumento { get; set; }
    public string tipoDocumento { get; set; }
    public string especieDocumento { get; set; }
    public string dataReferencia { get; set; }
    public string dataEntrega { get; set; }
    public string status { get; set; }
    public string descricaoStatus { get; set; }
    public string analisado { get; set; }
    public string situacaoDocumento { get; set; }
    public object assuntos { get; set; }
    public bool altaPrioridade { get; set; }
    public string formatoDataReferencia { get; set; }
    public int versao { get; set; }
    public string modalidade { get; set; }
    public string descricaoModalidade { get; set; }
    public string nomePregao { get; set; }
    public string informacoesAdicionais { get; set; }
    public string arquivoEstruturado { get; set; }
    public object formatoEstruturaDocumento { get; set; }
    public object nomeAdministrador { get; set; }
    public object cnpjAdministrador { get; set; }
    public object cnpjFundo { get; set; }
    public int idTemplate { get; set; }
    public object idSelectNotificacaoConvenio { get; set; }
    public int idSelectItemConvenio { get; set; }
    public bool indicadorFundoAtivoB3 { get; set; }
    public object idEntidadeGerenciadora { get; set; }
    public object ofertaPublica { get; set; }
    public object numeroEmissao { get; set; }
    public object tipoPedido { get; set; }
    public object dda { get; set; }
}
