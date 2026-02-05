## ProjectWork n.16 - MARTINO MATTEO 

Per la scrittura del Server è stato usato l'IDE Visual Studio 2026 e un progetto ASP.NET Core (linguaggio C#)
Per il corretto funzionamento si consiglia per tale ragione l'utilizzo del suddetto <a href="https://visualstudio.microsoft.com/it/">Visual Studio</a>.


  <summary>Requisiti:</summary>
  <ol>
    <li>Database PostgreSQL
      <ul>
        <li>Installare <a href="https://www.enterprisedb.com/downloads/postgres-postgresql-downloads">PostgreSQL</a></li>
      </ul>
        <li>Creazione di un file chiamato <code>.ENV</code> da posizionare nella cartella <code>ProjectWorkServer</code>
          con i dati inseriti in installazione così composto:
    <p><code>JWT_KEY=chiaveJWTdaALMENO32byte
 DB_HOST=localhost*
 DB_PORT=NumeroPorta*
 DB_NAME=NomeDatabase*
 DB_USER=NomeUtentePostgre*
 DB_PASS=PasswordPostgre*</code>
	<p>*Dati inerenti a PostgreSQL</p></p>
        </li>
    </li>
        <li>Pacchetti NuGet
			<p>Verificare esistenza dei seguenti pacchetti nel progetto <code>ProjectWorkServer > Dipendenze > Pacchetti</code></p>
        <ul>
          <li><a href="https://www.nuget.org/packages/DotNetEnv/3.1.1">DotNetEnv</a></li>          
          <li><a href="https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/10.0.2">Microsoft.AspNetCore.Authentication.JwtBearer</a></li>
          <li><a href="https://www.nuget.org/packages/Microsoft.AspNetCore.OpenApi/10.0.2">Microsoft.AspNetCore.OpenApi</a></li>
          <li><a href="https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/10.0.2">Microsoft.EntityFrameworkCore</a></li>
          <li><a href="https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/10.0.2">Microsoft.EntityFrameworkCore.Tools</a></li>
          <li><a href="https://www.nuget.org/packages/Npgsql/10.0.1">Npgsql</a></li>
           <li><a href="https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL/10.0.0">Npgsql.EntityFrameworkCore.PostgreSQL</a></li>
            <li><a href="https://www.nuget.org/packages/Swashbuckle.AspNetCore.Swagger">Swashbuckle.AspNetCore.Swagger</a></li>
            <li><a href="https://www.nuget.org/packages/Swashbuckle.AspNetCore.SwaggerGen">Swashbuckle.AspNetCore.SwaggerGen</a></li>
            <li><a href="https://www.nuget.org/packages/Swashbuckle.AspNetCore.SwaggerUI">Swashbuckle.AspNetCore.SwaggerUI</a></li>
        </ul>
        </li>
    </li>
  </ul>
