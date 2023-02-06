#import <UIKit/UIKit.h>

extern "C" {

void _impactOccurred(const char *style)
{

    UIImpactFeedbackStyle feedbackStyle;
    if (strcmp(style, "Heavy") == 0)
        feedbackStyle = UIImpactFeedbackStyleHeavy;
    else if (strcmp(style, "Medium") == 0)
        feedbackStyle = UIImpactFeedbackStyleMedium;
    else if (strcmp(style, "Light") == 0)
        feedbackStyle = UIImpactFeedbackStyleLight;
    else if (strcmp(style, "Rigid") == 0)
        if (@available(iOS 13.0, *)) {
            feedbackStyle = UIImpactFeedbackStyleRigid;
        } else {
            return;
        }
    else if (strcmp(style, "Soft") == 0)
        if (@available(iOS 13.0, *)) {
            feedbackStyle = UIImpactFeedbackStyleSoft;
        } else {
            return;
        }
    else
        return;

    UIImpactFeedbackGenerator *generator = [[UIImpactFeedbackGenerator alloc] initWithStyle:feedbackStyle];

    [generator prepare];

    [generator impactOccurred];
}

void _notificationOccurred(const char *style)
{
    UINotificationFeedbackType feedbackStyle;
    if (strcmp(style, "Error") == 0)
        feedbackStyle = UINotificationFeedbackTypeError;
    else if (strcmp(style, "Success") == 0)
        feedbackStyle = UINotificationFeedbackTypeSuccess;
    else if (strcmp(style, "Warning") == 0)
        feedbackStyle = UINotificationFeedbackTypeWarning;
    else
        return;

    UINotificationFeedbackGenerator *generator = [[UINotificationFeedbackGenerator alloc] init];

    [generator prepare];

    [generator notificationOccurred:feedbackStyle];
}

void _selectionChanged()
{
    UISelectionFeedbackGenerator *generator = [[UISelectionFeedbackGenerator alloc] init];

    [generator prepare];

    [generator selectionChanged];
}
}
