   M S B A M L   `   `   `  ���� I  EImageProcessor, Version=0.3.2.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:ImageProcessor.ConvertersImageProcessor.Converters  c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity      ImageProcessor.ImageWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     >
converters'clr-namespace:ImageProcessor.Converters     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     :xctk)http://schemas.xceed.com/wpf/xaml/toolkit       � Title$  Modificar imagen�� � WindowState$ 	Maximized=� � DataContext �� *ImageWindowViewModel+   	vmLocator �� Source# [   � 	Resources ;   5ImageProcessor.Converters.DoubleArrayToColorConverter  DoubleArrayToColorConverter8   2ImageProcessor.Converters.DoubleToPercentConverter  DoubleToPercentConverter9   3ImageProcessor.Converters.DoubleArrayToRGBConverter  DoubleArrayToRGBConverter��%-   &	       &	      &	 
           .���  � ColumnDefinitions �� $��* ��� $	��300 �.+�� -    �Name$ gridMain��.+��� -    ��Name$	 img�� J� RenderTransformOrigin$ 0.5,0.5&�$��10q����� *Bitmap+	 �� Mode$	 TwoWay=�z�V� .~��� 
 �� ScaleX
 ��  �� ElementName$ 
sliderZoom�� �� Path$ Value� �� ScaleY �� $ 
sliderZoom��$ Value�T� �� $��1��  BackGroundAccentColor#��[  �� CornerRadius$ 	10,0,0,10��._��  � RowDefinitions �� $��* ��� $��65 �.+��� $ 10,0,0,0��  BackGroundColor#��[ ._��� $��Visible=�.��� $��10q� �� $
��auto ��� $	��120 ��� $
��auto ��� $	��120 ��� $
��auto ��� $	��180 ��� .+��� $��K-Means��  TextBlockTitle#��[ $��	0,0,-10,0q�� $��1�� �� $��* ��� $��* ��� $��* ��� $��10 � �� �� �� .+��� $��	Clústers��  TextBlockOption#��[ �� $��	Distancia��#��[ $��1���� $��
Tolerancia��#��[ $��2���� $��1��$	��Top=�$��	10,0,10,0q����� *ClustersKMeans+$	 TwoWay=��� $��1��$��1��$	��Top=����� *TypeDistanceKMeans+ �� SelectedItem �� *TypeDistanceSelectedKMeans+$	 TwoWay=��� $��1��$	��Top=�$��	10,0,10,0q�$��2������ *ToleranceKMeans+$	 TwoWay=��� -    ��Name$ btnCalcKMeans��  ButtonStyleNext#��[ $��Calcular��$��2��$��2������ *KMeansCommand+�� $��3��$��Bottom=�$��3��$��1��
��������� $��K-Modes��$��2��#��[ $��	0,5,-10,0q�� $��3�� �� $��* ��� $��* ��� $��* ��� $��10 � �� �� .+��� $��	Clústers��#��[ �� $��
Tolerancia��#��[ $��2���� $��1��$	��Top=�$��	10,0,10,0q����� *ClustersKModes+$	 TwoWay=��� $��1��$	��Top=�$��	10,0,10,0q�$��2������ *ToleranceKmodes+$	 TwoWay=��� #��[ $��Calcular��$��2��$��2��$��Right=����� *KModesCommand+���� $ btnCalcKMeans��$ ActualWidth��� $��3��$��Bottom=�$��3��$��1��
��������� $��	MeanShift��$��4��#��[ $��	0,5,-10,0q�� $��5�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��8 � �� �� �� .+��� $��
10,10,10,0q�$��Kernel��#��[ �� $��
10,10,10,0q�$��Ancho��#��[ $��1���� $��
10,10,10,0q�$��Sigma��#��[ $��2���� $��0��$��1��$	��Top=����� *TypeKernelMeanS+ �� *TypeKernelSelectedMeanS+$	 TwoWay=��� $��1��$��1��$	��Top=�$��	10,0,10,0q����� *ConstKernelMeanS+$	 TwoWay=��� $��1��$��2��$	��Top=�$��	10,0,10,0q����� *
SigmaMeanS+$	 TwoWay=��� $��
10,10,10,0q�$��
Tolerancia��#��[ $��2��$��0���� $��
10,10,10,0q�$��Máx. iteraciones��#��[ $��2��$��2��$��1���� $��3��$��0��$	��Top=�$��	10,0,10,0q����� *ToleranceMeanS+$	 TwoWay=��� $��3��$��1��$��2��$	��Top=�$��	10,0,10,0q����� *IterationsMeanS+$	 TwoWay=��� #��[ $��Calcular��$��4��$��2��$��Right=����� *MeanShiftCommand+���� $ btnCalcKMeans��$ ActualWidth��� $��3��$��Bottom=�$��5��$��1��
��������� $��1��$��Bottom=�$��10q�.+��� #��[ �� $�Zoom x���� ��� $ 
sliderZoom��$ Value��� -    ��Name$ 
sliderZoom��$��1��$��Center=� � Minimum$ 1Q� � Maximum$ 30Q� �� IsSnapToTickEnabled . �  	 FloatingWindow#��[	 $��3��y��� *VisibilityProcessedBitmap+ �� $��50 ��� $��* � �� $��* ��� $	��300 �.+��� $��Center=�$��Center=�#��[ �� $�Cálculo�� �� ��� *Status+�� $��1�� 
 stlFlatButton#��[
 $��0��$��10q�$��Right=� � ToolTip$ Cerrar imagen������ *!VisibilityProcessedImageCommand+.���� $"��/Images/MainWindow/Close.png��$��25��.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ 
MouseEnter��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *MouseEnterCommand+  $ 
MouseLeave��.    �� *MouseLeaveCommand+�� #��[
 $��1��$
��40,0q�$��Right=�$ Guardar imagen������ *SaveImageCommand+.���� $!��/Images/MainWindow/Save.png��$��22��   $ 
MouseEnter��.    �� *MouseEnterCommand+  $ 
MouseLeave��.    �� *MouseLeaveCommand+�� $��1��$��0��$��5,0,5,20q����� *BitmapProcessed+�� $	��250��$��1��$��1��#��[	 $ 10��$��10q�._��  �� $��50 ��� $��* �.+��� $��Colores segmentados��#��[ $��Center=�$��Center=��� $��1��
������ 
������ $��
10,0,10,10q����� *ProportionsResult+���� .R�� $	��0,2q� �� $��* ��� $	��100 �.+���  ��Name$ colorPicker��$ 10��$��50��$��50��$! Pulsa para cambiar el color������ *Colors+ �� 	Converter# [    $ 	MouseDown��.    �� * DataContext.ChangeColorCommand+ �� RelativeSource ��  �� AncestorType$ ListBoxL�   CommandParameter �� $ colorPicker��$ 
Background�  $ 
MouseEnter��.    �� *DataContext.MouseEnterCommand+ �� $ ListBoxL�  $ 
MouseLeave��.    �� *DataContext.MouseLeaveCommand+ �� $ ListBoxL��� $��1��$��Center=�$��Center=�.+��� 
�������$��16(����� *
Proportion+# [ �� 
����������� *Colors+# [ 