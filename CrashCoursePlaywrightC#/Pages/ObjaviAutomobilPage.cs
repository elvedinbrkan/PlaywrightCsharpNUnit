using Microsoft.CodeAnalysis.Operations;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CrashCoursePlaywrightC_.Pages
{
    public class ObjaviAutomobilPage :BasePage
    {
        public ObjaviAutomobilPage(IPage page) : base(page) { }
        private ILocator PageTitleLbl => Page.GetByRole(AriaRole.Heading, new() { Name = "Objava oglasa u kategoriji Automobili" });
        private ILocator ProizvodjacDropdow => Page.GetByText("Odaberite proizvođača", new() {Exact= true });
        private ILocator AudiProizvodjacDropdownValue => Page.GetByText("Audi", new() { Exact = true });
        private ILocator ModelDropdown => Page.GetByText("Odaberite model", new () { Exact = true });
        private ILocator A3ModelDropdownValue => Page.GetByText("A3", new() { Exact = true });
        private ILocator SljedeciKorakBtn => Page.GetByText("Sljedeći korak");
        private ILocator NaslovOglasaTxtbox => Page.GetByRole(AriaRole.Textbox).Nth(1);
        private ILocator DizelGorivoBtn => Page.Locator("#buttonDizel");
        private ILocator GodisteDropdown => Page.GetByText("Izaberi godište", new () { Exact = true });
        private ILocator GodisteDropdownValue => Page.GetByText("2010", new() { Exact=true});
        private ILocator ManuelniTransmisijaBtn => Page.Locator("#buttonManuelni");
        private ILocator KilometrazaTxtbox => Page.Locator("input[name=\"kilometra-a\"]");
        private ILocator KubikazaDropdown => Page.GetByText("Izaberi kubikaža", new() { Exact = true });
        private ILocator KubikazaDropdownValue => Page.GetByText("1.6", new() { Exact = true });
        private ILocator SnagaMotoraTxtbox => Page.Locator("input[name=\"kilovata-kw\"]");
        private ILocator BrojVrataBtn => Page.Locator("button[id=\"button4/5\"]");
        private ILocator OpisTxtbox => Page.Locator("//div[@class=\"ql-editor ql-blank\"]");
        private ILocator OglasNijeDosupanOdmahSliderBtn => Page.Locator("//label[@class=\"switch\"]//div[@class=\"slider round\"]").Nth(0);
        private ILocator CijenaNaUpitSliderBtn => Page.Locator("//label[@class=\"switch\"]//div[@class=\"slider round\"]").Nth(1);
        private ILocator KonjskihSnagaTxtBox => Page.Locator("input[name=\"konjskih-snaga\"]");
        private ILocator MasaTezinaTxtBox => Page.Locator("input[name=\"masa-tezina-kg\"]");
        private ILocator PogonPrednjiBtn => Page.Locator("#buttonPrednji");
        private ILocator KlimatizacijaDvozonskaBtn => Page.Locator("#buttonDvozonska");
        private ILocator MuzikaCDBtn => Page.Locator("#buttonCD");
        private ILocator ParkingSenzoriNazadBtn => Page.Locator("#buttonNazad");
        private ILocator ParkingKameraNemaBtn => Page.Locator("//div[@id=6678]//button[@id=\"buttonNema\"]");
        private ILocator VrstaEnterijeraPlatnoBtn => Page.Locator("#buttonPlatno");
        private ILocator RoloZavjeseBocneBtn => Page.Locator("#buttonBočne");
        private ILocator SvjetlaLEDBtn => Page.Locator("#buttonLED");
        private ILocator SjedecihMjesta4Btn => Page.Locator("//div[@id=4167]//button[@id=\"button4\"]");
        private ILocator ZastitaElektricnaBlokadaBtn => Page.GetByText("Električna blokada", new() { Exact = true });
        private ILocator EmisioniStandardEuro5Btn => Page.GetByText("Euro 5", new() { Exact = true });
        private ILocator TipDropdown => Page.GetByText("Izaberi tip", new() { Exact = true });
        private ILocator TipDropdownValue => Page.GetByText("Malo auto", new() { Exact = true });
        private ILocator BojaDropdown => Page.GetByText("Izaberi boja", new() { Exact = true });
        private ILocator BojaDropdownValue => Page.GetByText("Crna", new() { Exact = true });
        private ILocator RegistrovanSlider => Page.Locator("label").Filter(new() { HasText = "Registrovan" }).Locator("div").Nth(1);
        private ILocator TempomatSlider => Page.Locator("label").Filter(new() { HasText="Tempomat"}).Locator("div").Nth(1);
        private ILocator StartStopSistemSlider => Page.Locator("label").Filter(new() { HasText = "Start-Stop sistem" }).Locator("div").Nth(1);
        private ILocator UbaciFotografijeBtn => Page.Locator("span").Filter(new LocatorFilterOptions() { HasText = "Klikni ovdje da ubaciš fotografije" }).Locator("label");
        private ILocator ZavrsiObjavuOglasaBtn => Page.GetByText("Završi objavu oglasa");
        private ILocator PoljeJeObaveznoLbl => Page.GetByText("Polje je obavezno");


        public async Task<bool> IsPageTitleVisible() => await IsWebElementVisible(PageTitleLbl);

        public async Task<string> GetPageTitle()
        {
            return await PageTitleLbl.InnerTextAsync();
        }

        public async Task SelectProizvodjac_i_Model()
        {
            await ProizvodjacDropdow.ClickAsync();
            await AudiProizvodjacDropdownValue.ClickAsync();
            await ModelDropdown.ClickAsync();
            await A3ModelDropdownValue.ClickAsync();
            await SljedeciKorakBtn.ClickAsync();
        }
        public async Task SelectProizvodjac(string Proizvodjac)
        {
            await ProizvodjacDropdow.ClickAsync();
            await Page.GetByText(Proizvodjac, new() { Exact = true }).ClickAsync(); 
            //locating the element from dropdown based on input from test
        }
        public async Task SelectModel(string Model)
        {
            await ModelDropdown.ClickAsync();
            await Page.GetByText(Model, new() { Exact = true }).ClickAsync();
            await SljedeciKorakBtn.ClickAsync();
            //locating the element from dropdown based on input from test
        }

        public async Task SelectObavezneInformacije() //receive values here such as model, godiste.. and use them in method
        {
            await NaslovOglasaTxtbox.FillAsync("Audi A3 1.6 TDI REGISTROVAN BEZ ULAGANJA MAX UTEGNUT");
            await GodisteDropdown.ClickAsync();
            await GodisteDropdownValue.ClickAsync();
            await DizelGorivoBtn.ClickAsync();
            await ManuelniTransmisijaBtn.ClickAsync();
            await KilometrazaTxtbox.FillAsync("310000");
            await KubikazaDropdown.ClickAsync();
            await KubikazaDropdownValue.ClickAsync();
            await SnagaMotoraTxtbox.FillAsync("77");
            await BrojVrataBtn.ClickAsync();
            await OpisTxtbox.FillAsync("Prodaje se Audi A3 1.6 TDI.\n Svi servisi uradjeni.");
            await SljedeciKorakBtn.ClickAsync();

        }

        public async Task SelectOsnovniPodaci()
        {
            await OglasNijeDosupanOdmahSliderBtn.ClickAsync();
            await CijenaNaUpitSliderBtn.ClickAsync();
            await SljedeciKorakBtn.ClickAsync();
        }

        public async Task SelectDodatneInformacije()
        {
            await KonjskihSnagaTxtBox.FillAsync("105");
            await MasaTezinaTxtBox.FillAsync("1450");
            await PogonPrednjiBtn.ClickAsync();
            await KlimatizacijaDvozonskaBtn.ClickAsync();
            await MuzikaCDBtn.ClickAsync();
            await ParkingSenzoriNazadBtn.ClickAsync();
            await ParkingKameraNemaBtn.ClickAsync();   
            await VrstaEnterijeraPlatnoBtn.ClickAsync();
            await RoloZavjeseBocneBtn.ClickAsync();
            await SvjetlaLEDBtn.ClickAsync();
            await SjedecihMjesta4Btn.ClickAsync();
            await ZastitaElektricnaBlokadaBtn.ClickAsync();
            await EmisioniStandardEuro5Btn.ClickAsync();
            await TipDropdown.ClickAsync();
            await TipDropdownValue.ClickAsync();
            await BojaDropdown.ClickAsync();
            await BojaDropdownValue.ClickAsync();
            await RegistrovanSlider.ClickAsync();
            await TempomatSlider.ClickAsync();
            await StartStopSistemSlider.ClickAsync();
            await SljedeciKorakBtn.ClickAsync();
        }

        public async Task<MojiOglasiPage> SelectFotografije()
        {
            await UbaciFotografijeBtn.SetInputFilesAsync(new[] { "audia3.jpg", "audia3interior.png" });
            await ZavrsiObjavuOglasaBtn.ClickAsync();
            return new MojiOglasiPage(Page);
        }

        public async Task PartialSelectObavezneInformacije() //receive values here such as model, godiste.. and use them in method
        {
            await NaslovOglasaTxtbox.FillAsync("Audi A3 1.6 TDI REGISTROVAN BEZ ULAGANJA MAX UTEGNUT");
            //await GodisteDropdown.ClickAsync();
            //await GodisteDropdownValue.ClickAsync();
            await DizelGorivoBtn.ClickAsync();
            await ManuelniTransmisijaBtn.ClickAsync();
            await KilometrazaTxtbox.FillAsync("310000");
            await KubikazaDropdown.ClickAsync();
            await KubikazaDropdownValue.ClickAsync();
            await SnagaMotoraTxtbox.FillAsync("77");
            await BrojVrataBtn.ClickAsync();
            await OpisTxtbox.FillAsync("Prodaje se Audi A3 1.6 TDI.\n Svi servisi uradjeni.");
            await SljedeciKorakBtn.ClickAsync();
        }

        public async Task<bool> IsPoljeJeObaveznoLblVisible() => await IsWebElementVisible(PoljeJeObaveznoLbl);

        public async Task<string> GetMandatoryErrorMsg()
        {
            return await PoljeJeObaveznoLbl.InnerTextAsync();
        }
    }
}
