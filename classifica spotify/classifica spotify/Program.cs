using static System.Runtime.InteropServices.JavaScript.JSType;

string[] spotify = File.ReadAllLines("classifica.txt");

List<string> list = new List<string>(spotify);
list.RemoveAt(0);

List<string> brani_art(List<string> list, string artista)
{
    List<string> brani = new List<string>();
    foreach (string s in list)
    {
        string[] riga = s.Split('|');
        if (s.Contains(artista))
        {
            brani.Add($"{riga[1]} - {riga[2]}");
        }
    }
    return brani;
}

void stampa_list(List<string> list)
{
    foreach (string s in list)
    {
        Console.WriteLine(s);
    }
}

List<string> super_streams(List<string> list, ref string art, ref string anno)
{
    List<string> ss = new List<string>();
    foreach (string s in list)
    {
        string[] riga = s.Split('|');
        if (long.Parse(riga[4]) > 3000000000)
        {
            ss.Add(riga[1]);
        }
        if (riga[2].Contains("&"))
        {
            string[] a4 = riga[2].Split(" & ");
            foreach (string x in a4)
            {

                if (!art.Contains(x))
                {
                    art += x + ",";
                }
            }
        }
        else if (!art.Contains(riga[2]))
        {
            art += riga[2] + ",";
        }
        if (!anno.Contains(riga[3]))
        {
            anno += riga[3] + ",";
        }
    }
    return ss;
}

int[] num_brani(List<string> list, List<string> artista)
{
    int[] n_ar = new int[artista.Count];
    foreach (string s in list)
    {
        string[] riga = s.Split('|');
        for (int i = 0; i < artista.Count; i++)
        {
            if (riga[2].Contains("&"))
            {
                string[] a4 = riga[2].Split(" & ");

                for (int j = 0; j < a4.Length; j++)
                {
                    if (a4[j] == artista[i])
                    {
                        n_ar[i]++;
                    }
                }
            }
            else if (riga[2] == artista[i])
            {
                n_ar[i]++;
            }
        }
    }
    return n_ar;
}

StreamWriter writer3 = new StreamWriter("anni.txt");

void brani_anno(List<int> a, List<string> list)
{
    foreach (int x in a)
    {
        writer3.WriteLine($"Anno:{x}");

        foreach (string s in list)
        {
            string[] riga = s.Split('|');
            if (x.ToString() == riga[3])
            {
                writer3.WriteLine($"{riga[1]} - {riga[2]}");
            }
        }
        writer3.WriteLine();
    }
}

Console.WriteLine("inserisci il nome di un artista");
string nome = Console.ReadLine();

List<string> brani = brani_art(list, nome);
stampa_list(brani);

File.WriteAllLines("titoli_artista.txt", brani);

string nomi = "", anni = "";
List<string> ss = super_streams(list, ref nomi, ref anni);

File.WriteAllLines("super_streams.txt", ss);

List<string> nomi_art = new List<string>(nomi.Substring(0, nomi.Length - 1).Split(","));
List<string> anni_a = new List<string>(anni.Substring(0, anni.Length - 1).Split(","));
List<int> a = new List<int>();

foreach (string s in anni_a)
{
    a.Add(int.Parse(s));
}

a.Sort();

List<int> numeri_brani_art = new List<int>(num_brani(list, nomi_art));

for (int i = 0; i < nomi_art.Count; i++)
{
    Console.WriteLine($"{nomi_art[i]} - {numeri_brani_art[i]}");
}

int max = 0;
foreach (int s in numeri_brani_art)
{
    if (s > max)
    {
        max = s;
    }
}

string max_ar = nomi_art[numeri_brani_art.IndexOf(max)];

Console.WriteLine($"artista con più brani in classifica: {max_ar}");
Console.WriteLine();

brani_anno(a, list);

writer3.Close();