# Module 4: Running Functional Tests with Azure Pipeline & Artifacts

Please [view and download](https://github.com/Gwayaboy/Module4-AzurePipelinesAndArtifacts/blob/master/Content/RunningAutomationTestsWithAzurePipelines-Module%204.pdf) Module 4's Slide deck

## Agenda

 1.  **[Azure Pipelines](https://docs.microsoft.com/en-us/azure/devops/pipelines/?view=azure-devops)**
     - [Repository](https://docs.microsoft.com/en-us/azure/devops/pipelines/repos/?view=azure-devops)
     - [Agents types](https://docs.microsoft.com/en-us/azure/devops/pipelines/agents/pools-queues?view=azure-devops&tabs=yaml%2Cbrowser
) 
     - [UI Testing consideration](https://docs.microsoft.com/en-us/azure/devops/pipelines/test/ui-testing-considerations?view=azure-devops&tabs=mstest)
     - [Labs#1](https://github.com/Gwayaboy/Module4-AzurePipelinesAndArtifacts/blob/master/README.md#building-an-azure-devops-pipeline-for-running-selenium-functional-tests) : Create YAML Pipeline to build, release and run your functional UI Tests
  
 2. **Azure Artifacts**
    - [Overview](https://docs.microsoft.com/en-us/azure/devops/pipelines/artifacts/artifacts-overview?view=azure-devops) 
    - [Package](https://docs.microsoft.com/en-us/nuget/create-packages/creating-a-package) your test libary, [publish](https://docs.microsoft.com/en-us/azure/devops/pipelines/artifacts/nuget?view=azure-devops&tabs=yaml) and [share](https://docs.microsoft.com/en-us/azure/devops/pipelines/packages/nuget-restore?view=azure-devops)
 3. **Discussions & Wrap-up**
    - Q&A / Discussions
    - Wrap-up
 4. **Next steps**
    - [FeedBack for the session](https://aka.ms/PipelinesArtifacts)

    

## Hands-on Labs

   #### Set up your Azure DevOps Organisation

  1.	Use or [create](https://signup.live.com) your personal Microsoft Account (MSA)      
  2.	[Create a free Azure DevOps organization](https://dev.azure.com/)  associated with your MSA

  3. Create a Project by clicking on the top right corning New Project button 

      ![](https://demosta.blob.core.windows.net/images/NewDevOpsProject.PNG)

  4. Set its visibility to public and name it AzurePipelineFunctionalTestsLab 

  5. optionally provide with a description such as _```"Build and run Web UI Functional Tests for Bing Web Search"```_

  #### Building an Azure DevOps Pipeline for running Selenium Functional Tests

  1. **Import and setup GitHub Repo to Azure DevOps Repository**
      - Click on the Repos on the left side Pane
      - Click on Import repository button to bring up **Import a Git repository** panel
      ![](https://demosta.blob.core.windows.net/images/ImportGitRepo.PNG)
        
        - Paste Module 2's repository [https://github.com/Gwayaboy/Module2-UIAutomationTesting.git](https://github.com/Gwayaboy/Module2-UIAutomationTesting.git) to clone
        - Click import button this will takes a few seconds to a minute until you get a succesfull repository message
          ![](https://demosta.blob.core.windows.net/images/ImportRepoSuccess.PNG)

        - From the top branch drop down switch to the Finished Branch

          ![](https://demosta.blob.core.windows.net/images/SelectFinishedBranch.PNG)


  2.  **Let's set up a new YAML Build by clicking on the top right button**
      
      ![](https://demosta.blob.core.windows.net/images/NewBuild.PNG)
      
      - This will bring us to the configure pipeline Wizzard

        ![](https://demosta.blob.core.windows.net/images/ConfigurePipelineWizzard.PNG)

        - Click  on show more and Select ASP.NET template

          ![](https://demosta.blob.core.windows.net/images/ASPNETYAMLTemplate.PNG)

        - You will be presented with a YAML build pipeline definition unsaved definition azure-pipeline.yaml file at the root of the repository
          ```YAML
          # ASP.NET
          # Build and test ASP.NET projects.
          # Add steps that publish symbols, save build artifacts, deploy, and more:
          # https://docs.microsoft.com/azure/devops/pipelines/apps/aspnet/build-aspnet-4

          trigger:
          - master

          pool:
            vmImage: 'windows-latest'

          variables:
            solution: '**/*.sln'
            buildPlatform: 'Any CPU'
            buildConfiguration: 'Release'

          steps:
          - task: NuGetToolInstaller@1

          - task: NuGetCommand@2
            inputs:
              restoreSolution: '$(solution)'

          - task: VSBuild@1
            inputs:
              solution: '$(solution)'
              msbuildArgs: '/p:DeployOnBuild=true /p:WebPublishMethod=Package /p:PackageAsSingleFile=true /p:SkipInvalidConfigurations=true /p:PackageLocation="$(build.artifactStagingDirectory)"'
              platform: '$(buildPlatform)'
              configuration: '$(buildConfiguration)'

          - task: VSTest@2
            inputs:
              platform: '$(buildPlatform)'
              configuration: '$(buildConfiguration)'
          ```

        - Let's change the name and location of our YAML file to
        ```Sources/Exercices/2-Selenium_Page_Objects/BingSearchPageObjects/build.yml```
        - Next let's only target to our BingSearchPageObjects.sln by changing the variables section with
          ```YAML
          variables:
              solution: '**/Sources/Exercices/2-Selenium_Page_Objects/*.sln'
              buildPlatform: 'Any CPU'
              buildConfiguration: 'Release'
          ```

        - Place the cussor to the end of the YAML file with no idendation
        - Bring up the Assistance panel by clicking 
        
          ![](https://demosta.blob.core.windows.net/images/ShowAssistance.PNG)
        - Then let's remove the second ```VSTest@2``` task since we don't have unit or shallow integration tests to run as part of our continuous integration build

        - Let's add a copy files task to only copy the relevant DLLs so that we can run our Selenium tests later

          ![](https://demosta.blob.core.windows.net/images/Tasks.PNG)

          - Select Copy files task
         
          - Then define Source Folder with ```Sources/Exercices/2-Selenium_Page_Objects/BingSearchPageObjects/bin``` and Target folder with ```$(build.artifactstagingdirectory)``` which refers to the release staging root folder. 

           
            ![](https://demosta.blob.core.windows.net/images/CopyFilesTask.PNG)
          
            Once you click on the Add button on the panel an additional task will be generated as below 

            ```YAML
            - task: CopyFiles@2
              inputs:
              Contents: '**'
              SourceFolder: 'Sources/Exercices/2-Selenium_Page_Objects/BingSearchPageObjects/bin'
              TargetFolder: '$(build.artifactstagingdirectory)'
            ```

        - Then finally following same steps, bring up assistant panel search and select the _"```publish build artifacts```"_ task 
        - Since the default parameters are good enough, just click on the Add button to generate an additional task as follow:
          ```YAML
          - task: PublishBuildArtifacts@1
            inputs:
              PathtoPublish: '$(Build.ArtifactStagingDirectory)'
              ArtifactName: 'drop'
              publishLocation: 'Container'
          ```
  3. **Click on the Save and run top right button to save our build definition and run it**

      ![](https://demosta.blob.core.windows.net/images/SaveAndRunBuild.PNG)

      - A dialog panel appears to confirm default options to commit directly tothe Finished branch.
      - Just click again on the Save and run bottom right button of the panel

        ![](https://demosta.blob.core.windows.net/images/SaveAndRunCOnfirmationDialog.PNG)

        
  4. **Watch the Job run streamed logs and if everything has worked correctly you should have a successful Build run**

    Click on the Build number top path

    ![](https://demosta.blob.core.windows.net/images/JobRun.PNG)

    You should then see your build summary.
    Click on the 1 published pipeline artifact


    ![](https://demosta.blob.core.windows.net/images/BuildSummary.PNG) 

    You should get redirect the artifacts page and if you expand the drop artifact you will have something as below:

    ![](https://demosta.blob.core.windows.net/images/PublishedPipelineArtifacts.PNG)